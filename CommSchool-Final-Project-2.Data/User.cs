namespace CommSchool_Final_Project_2.Data;

public class User
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public string Mail { get; set; }
    public double MonthlyIncome { get; set; }
    public bool IsBlocked { get; set; }
    public string Role { get; set; }
    public List<Loan> Loan { get; set; } = [];
}

public static class Role
{
    public const string Admin = "Admin";
    public const string User = "User";
}