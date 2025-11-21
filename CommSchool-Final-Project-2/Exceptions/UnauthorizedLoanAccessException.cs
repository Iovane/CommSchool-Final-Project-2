namespace CommSchool_Final_Project_2.Exceptions;

public class UnauthorizedLoanAccessException : Exception
{
    public UnauthorizedLoanAccessException() : base("You are not allowed to access this loan")
    {
    }

    public UnauthorizedLoanAccessException(string message) : base(message)
    {
    }
}
