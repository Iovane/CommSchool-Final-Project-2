using CommSchool_Final_Project_2.Data;

namespace CommSchool_Final_Project_2.DTOs;

public class LoanDto
{
    public required double Amount { get; set; }
    public required string Currency { get; set; }
    public required int Term { get; set; }
    public required double InterestRate { get; set; }
    public required double MonthlyPayment { get; set; }
    public required Status Status { get; set; } = Status.InProgress;
}