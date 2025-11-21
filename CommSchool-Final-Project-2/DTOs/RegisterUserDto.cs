namespace CommSchool_Final_Project_2.DTOs;

public class RegisterUserDto
{
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required int Age { get; set; }
    public required string Mail { get; set; }
    public required double MonthlyIncome { get; set; }
}