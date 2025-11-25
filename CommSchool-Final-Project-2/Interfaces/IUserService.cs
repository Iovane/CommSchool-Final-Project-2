using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.DTOs;

namespace CommSchool_Final_Project_2.Interfaces;

public interface IUserService
{
    User Login(LoginUserDto model);
    User RegisterUser(RegisterUserDto registerUser);
    User GetUserById(int id);
    void BlockUser(int id);
    void UnblockUser(int id);
}