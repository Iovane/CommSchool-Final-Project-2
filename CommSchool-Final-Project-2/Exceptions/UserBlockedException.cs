namespace CommSchool_Final_Project_2.Exceptions;

public class UserBlockedException : Exception
{
    public UserBlockedException() : base("You are blocked from requesting loans")
    {
    }

    public UserBlockedException(string message) : base(message)
    {
    }
}
