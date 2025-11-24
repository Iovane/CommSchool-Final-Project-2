using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.Domain;
using CommSchool_Final_Project_2.DTOs;
using CommSchool_Final_Project_2.Exceptions;
using CommSchool_Final_Project_2.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class LoanServiceTests
{
    private readonly LoanService _loanService;
    private readonly AppDbContext _context;

    public LoanServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _loanService = new LoanService(_context);
    }
    
    [Fact]
    public void RequestLoan_UserNotFound_ThrowsException()
    {
        var loanDto = new RequestedLoanDto { LoanType = LoanType.FastConsumer, Term = 12 };
        Assert.Throws<UserNotFoundException>(() => _loanService.RequestLoan(loanDto, userId: 99));
    }
    
    [Fact]
    public void RequestLoan_UserBlocked_ThrowsException()
    {
        _context.Users.Add(new User { Id = 1, IsBlocked = true });
        _context.SaveChanges();

        var loanDto = new RequestedLoanDto { LoanType = LoanType.FastConsumer, Term = 12 };
        Assert.Throws<UserBlockedException>(() => _loanService.RequestLoan(loanDto, 1));
    }

    [Fact]
    public void RequestLoan_ValidUser_ReturnsLoan()
    {
        _context.Users.Add(new User { Id = 1, MonthlyIncome = 5000 });
        _context.SaveChanges();

        var dto = new RequestedLoanDto { LoanType = LoanType.FastConsumer , Term = 12 };
        var loan = _loanService.RequestLoan(dto, 1);

        loan.Should().NotBeNull();
        loan.UserId.Should().Be(1);
    }
    
    [Fact]
    public void GetLoans_UserRole_ReturnsOwnLoans()
    {
        _context.Loans.AddRange(
            new Loan { Id = 1, UserId = 1, Currency = "GEL", Status = Status.InProgress },
            new Loan { Id = 2, UserId = 2, Currency = "GEL", Status = Status.InProgress }
        );
        _context.SaveChanges();

        var loans = _loanService.GetLoans(1, UserRole.User);
        
        Assert.Single(loans);
        loans[0].UserId.Should().Be(1);
    }
    
    [Fact]
    public void GetLoans_AccountantRole_ReturnsAllLoans()
    {
        _context.Loans.AddRange(
            new Loan { Id = 1, UserId = 1, Currency = "GEL", Status = Status.InProgress },
            new Loan { Id = 2, UserId = 2, Currency = "GEL", Status = Status.InProgress }
        );
        _context.SaveChanges();

        var loans = _loanService.GetLoans(1, UserRole.Accountant);
        loans.Count.Should().Be(2);
    }

    [Fact]
    public void GetLoan_LoanNotFound_ThrowsException()
    {
        Assert.Throws<LoanNotFoundException>(() => _loanService.GetLoan(99, 1, UserRole.User));
    }
    
    [Fact]
    public void GetLoan_UnauthorizedUser_ThrowsException()
    {
        _context.Loans.Add(new Loan { Id = 1, UserId = 2, Currency = "GEL", Status = Status.InProgress });
        _context.SaveChanges();

        Assert.Throws<UnauthorizedLoanAccessException>(() => _loanService.GetLoan(1, 1, UserRole.User));
    }
    
    [Fact]
    public void GetLoan_AccountantAccess_ReturnsLoan()
    {
        _context.Loans.Add(new Loan { Id = 1, UserId = 2, Currency = "GEL", Status = Status.InProgress });
        _context.SaveChanges();

        var loan = _loanService.GetLoan(1, 1, UserRole.Accountant);
        loan.Id.Should().Be(1);
    }
    
    [Fact]
    public void UpdateLoan_ValidData_UpdatesLoan()
    {
        var loan = new Loan { Id = 1, UserId = 1, Currency = "GEL", Status = Status.InProgress };
        _context.Loans.Add(loan);
        _context.SaveChanges();

        var dto = new LoanDto
        {
            Amount = 10000,
            Currency = "USD",
            Term = 24,
            InterestRate = 5.5,
            MonthlyPayment = 450,
            Status = Status.Approved
        };

        var updated = _loanService.UpdateLoan(dto, 1, 1, UserRole.User);

        updated.Amount.Should().Be(10000);
        updated.Currency.Should().Be("USD");
        updated.Term.Should().Be(24);
        updated.InterestRate.Should().Be(5.5);
        updated.MonthlyPayment.Should().Be(450);
        updated.Status.Should().Be(Status.Approved);
    }
    
    [Fact]
    public void UpdateLoan_UnauthorizedUser_ThrowsException()
    {
        var loan = new Loan { Id = 1, UserId = 2, Currency = "GEL", Status = Status.InProgress };
        _context.Loans.Add(loan);
        _context.SaveChanges();

        var dto = new LoanDto
        {
            Amount = 10000,
            Currency = "USD",
            Term = 24,
            InterestRate = 5.5,
            MonthlyPayment = 450,
            Status = Status.Approved
        };

        Assert.Throws<UnauthorizedLoanAccessException>(() =>
            _loanService.UpdateLoan(dto, 1, 1, UserRole.User));
    }
    
    [Fact]
    public void DeleteLoan_AccountantRole_DeletesLoan()
    {
        var loan = new Loan { Id = 1, UserId = 1, Currency = "GEL", Status = Status.InProgress };
        _context.Loans.Add(loan);
        _context.SaveChanges();

        _loanService.DeleteLoan(1, 1, UserRole.Accountant);

        _context.Loans.Should().BeEmpty();
    }
    
    [Fact]
    public void DeleteLoan_UserRoleApprovedLoan_ThrowsException()
    {
        var loan = new Loan { Id = 1, UserId = 1, Currency = "GEL", Status = Status.Approved };
        _context.Loans.Add(loan);
        _context.SaveChanges();

        Assert.Throws<UnauthorizedLoanAccessException>(() =>
            _loanService.DeleteLoan(1, 1, UserRole.User));
    }
    
    [Fact]
    public void DeleteLoan_UserRoleRejectedLoan_ThrowsException()
    {
        var loan = new Loan { Id = 1, UserId = 1, Currency = "GEL", Status = Status.Rejected };
        _context.Loans.Add(loan);
        _context.SaveChanges();
        
        Assert.Throws<UnauthorizedLoanAccessException>(() =>
            _loanService.DeleteLoan(1, 1, UserRole.User));
    }
    
    [Fact]
    public void DeleteLoan_UserRoleInProgressLoan_DeletesLoan()
    {
        var loan = new Loan { Id = 1, UserId = 1, Currency = "GEL", Status = Status.InProgress };
        _context.Loans.Add(loan);
        _context.SaveChanges();

        _loanService.DeleteLoan(1, 1, UserRole.User);

        _context.Loans.Should().BeEmpty();
    }
}