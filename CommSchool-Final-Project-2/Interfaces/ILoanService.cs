using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.DTOs;

namespace CommSchool_Final_Project_2.Interfaces;

public interface ILoanService
{
    Loan RequestLoan(RequestedLoanDto loanDto, int userId);
    List<Loan> GetLoans(int userId, string userRole);
    Loan GetLoan(int loanId, int userId, string userRole);
    Loan UpdateLoan(LoanDto updatedLoan, int loanId, int userId, string userRole);
    void DeleteLoan(int loanId, int userId, string userRole);
}