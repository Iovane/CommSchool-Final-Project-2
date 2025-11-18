namespace CommSchool_Final_Project_2.Data;

public class Loan
{
    public int Id { get; set; }
    public LoanType LoanType { get; set; }
    public double Amount { get; set; }
    public string Currency { get; set; }
    public int Term { get; set; }
    public double InterestRate { get; set; }
    public double MonthlyPayment { get; set; }
    public Status Status { get; set; } = Status.InProgress;
    public int UserId { get; set; }
}