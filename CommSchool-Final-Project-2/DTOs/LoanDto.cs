using CommSchool_Final_Project_2.Data;

namespace CommSchool_Final_Project_2.DTOs;

public class LoanDto
{
    public double Amount { get; set; }
    public string Currency { get; set; }
    public int Term { get; set; }
    public double InterestRate { get; set; }
    public double MonthlyPayment { get; set; }
    public Status Status { get; set; } = Status.InProgress;
}