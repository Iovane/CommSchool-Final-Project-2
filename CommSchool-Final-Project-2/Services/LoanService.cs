using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.Domain;
using CommSchool_Final_Project_2.DTOs;
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

    public Loan RequestLoan(RequestedLoanDto requestedLoanDto, string? userId)
    {
        var userIncome = _context.Users.Find(Convert.ToInt32(userId))?.MonthlyIncome ?? 0;
        var loan = LoanOriginationSystem.CalculateLoan(userIncome, requestedLoanDto.LoanType, requestedLoanDto.Term, userId);
        
        _context.Loans.Add(loan);
        _context.SaveChanges();
        
        return loan;
    }

    public List<Loan> GetLoans()
    {
        throw new NotImplementedException();
    }

    public Loan UpdateLoan(Loan updatedLoan)
    {
        throw new NotImplementedException();
    }

    public bool DeleteLoan(int loanId)
    {
        throw new NotImplementedException();
    }
}