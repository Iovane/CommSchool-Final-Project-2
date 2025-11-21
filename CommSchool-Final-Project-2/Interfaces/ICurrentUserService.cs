namespace CommSchool_Final_Project_2.Interfaces;

public interface ICurrentUserService
{
    int UserId { get; }
    string Role { get; }
}
