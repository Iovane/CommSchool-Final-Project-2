using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.DTOs;

namespace CommSchool_Final_Project_2.Interfaces;

public interface ILoanService
{
    Loan RequestLoan(RequestedLoanDto loanDto, string userId);
    List<Loan> GetLoans(string userId);
    Loan UpdateLoan(Loan updatedLoan);
    bool DeleteLoan(int loanId);
}