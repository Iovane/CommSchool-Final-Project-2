using CommSchool_Final_Project_2.Data;

namespace CommSchool_Final_Project_2.DTOs;

public class RequestedLoanDto
{
    public LoanType LoanType { get; set; }
    public int Term { get; set; }
}