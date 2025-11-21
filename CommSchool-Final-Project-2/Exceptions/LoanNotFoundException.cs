namespace CommSchool_Final_Project_2.Exceptions;

public class LoanNotFoundException : Exception
{
    public LoanNotFoundException() : base("Loan not found")
    {
    }

    public LoanNotFoundException(string message) : base(message)
    {
    }
}
