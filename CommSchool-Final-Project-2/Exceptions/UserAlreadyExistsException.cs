namespace CommSchool_Final_Project_2.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException() : base("User already exists")
    {
    }

    public UserAlreadyExistsException(string message) : base(message)
    {
    }
}
