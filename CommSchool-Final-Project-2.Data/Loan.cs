namespace CommSchool_Final_Project_2.Data;

public class Loan
{
    public int Id { get; set; }
    public int LoanTypeId { get; set; }
    public double Amount { get; set; }
    public string Currency { get; set; }
    public int Term { get; set; }
    public int StatusId { get; set; }
    public int UserId { get; set; }
}