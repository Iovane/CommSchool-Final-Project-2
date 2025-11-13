using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.DTOs;
using Homework_19.Models;

namespace CommSchool_Final_Project_2.Interfaces;

public interface IUserService
{
    User? Login(LoginUserDto model);
    User? RegisterUser(RegisterUserDto registerUser);
}