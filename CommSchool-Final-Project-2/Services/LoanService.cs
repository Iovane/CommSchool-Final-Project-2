using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.Domain;
using CommSchool_Final_Project_2.DTOs;
using CommSchool_Final_Project_2.Exceptions;
using CommSchool_Final_Project_2.Helpers;
using CommSchool_Final_Project_2.Interfaces;

namespace CommSchool_Final_Project_2.Services;

public class LoanService : ILoanService
{
    private readonly AppDbContext _context;

    public LoanService(AppDbContext context)
    {
        _context = context;
    }

    public Loan RequestLoan(RequestedLoanDto requestedLoanDto, int userId)
    {
        var user = _context.Users.Find(userId);
        if (user is null)
            throw new UserNotFoundException();

        if (user.IsBlocked)
            throw new UserBlockedException();

        var loan = LoanOriginationSystem.CalculateLoan(user.MonthlyIncome, requestedLoanDto.LoanType,
            requestedLoanDto.Term, userId);

        _context.Loans.Add(loan);
        _context.SaveChanges();

        return loan;
    }

    public List<Loan> GetLoans(int userId, string userRole)
    {
        var loans = userRole is UserRole.User
            ? _context.Loans.Where(loan => loan.UserId == userId).ToList()
            : _context.Loans.ToList();

        return loans;
    }

    public Loan GetLoan(int loanId, int userId, string userRole)
    {
        var loan = _context.Loans.FirstOrDefault(l => l.Id == loanId);

        if (loan is null)
            throw new LoanNotFoundException();

        if (loan.UserId != userId && userRole == UserRole.User)
            throw new UnauthorizedLoanAccessException();

        return loan;
    }

    public Loan UpdateLoan(LoanDto updatedLoan, int loanId, int userId, string userRole)
    {
        var loan = GetLoan(loanId, userId, userRole);

        loan.Amount = updatedLoan.Amount;
        loan.Currency = updatedLoan.Currency;
        loan.Term = updatedLoan.Term;
        loan.InterestRate = updatedLoan.InterestRate;
        loan.MonthlyPayment = updatedLoan.MonthlyPayment;
        loan.Status = updatedLoan.Status;

        _context.Loans.Update(loan);
        _context.SaveChanges();

        return loan;
    }

    public void DeleteLoan(int loanId, int userId, string userRole)
    {
        var loan = GetLoan(loanId, userId, userRole);

        if (userRole is UserRole.User && loan.Status is Status.Approved or Status.Rejected)
            throw new UnauthorizedLoanAccessException(ExceptionMessages.DeleteForbidden);

        _context.Loans.Remove(loan);
        _context.SaveChanges();
    }
}