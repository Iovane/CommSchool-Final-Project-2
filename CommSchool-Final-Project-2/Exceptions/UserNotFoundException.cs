namespace CommSchool_Final_Project_2.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("User not found")
    {
    }

    public UserNotFoundException(string message) : base(message)
    {
    }
}
