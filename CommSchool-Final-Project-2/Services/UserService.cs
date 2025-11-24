using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.Domain;
using CommSchool_Final_Project_2.DTOs;
using CommSchool_Final_Project_2.Exceptions;
using CommSchool_Final_Project_2.Helpers;
using CommSchool_Final_Project_2.Interfaces;

namespace CommSchool_Final_Project_2.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public User RegisterUser(RegisterUserDto registerUser)
    {
        if (_context.Users.Any(u => u.Username == registerUser.Username))
            throw new UserAlreadyExistsException();
        
        var user = new User
        {
            Firstname = registerUser.Firstname,
            Lastname = registerUser.Lastname,
            Username = registerUser.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(registerUser.Password),
            Age = registerUser.Age,
            Mail = registerUser.Mail,
            MonthlyIncome = registerUser.MonthlyIncome
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        
        return user;
    }

    public User Login(LoginUserDto loginModel)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Username == loginModel.Username);
        
        if (user is null) 
            throw new UserNotFoundException();
        
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password);
        return !isPasswordValid ? throw new UnauthorizedAccessException(ExceptionMessages.InvalidCredentials) : user;
    }

    public User GetUserInfo(int id)
    {
        var user = _context.Users.Find(id);
       
        return user ?? throw new UserNotFoundException();
    }

    public User GetUserById(int id, string userRole)
    {
        if (userRole is not UserRole.Accountant)
            throw new UnauthorizedAccessException(ExceptionMessages.UserNotAuthorized);
        
        var user = _context.Users.Find(id);
        
        return user ?? throw new UserNotFoundException();
    }

    public void BlockUser(int id, string userRole)
    {
        if (userRole is not UserRole.Accountant)
            throw new UnauthorizedAccessException(ExceptionMessages.UserNotAuthorized);
        
        var user = GetUserById(id, userRole);
        user.IsBlocked = true;
        
        _context.Users.Update(user);
        _context.SaveChanges();

    }

    public void UnblockUser(int id, string userRole)
    {
        if (userRole is not UserRole.Accountant)
            throw new UnauthorizedAccessException(ExceptionMessages.UserNotAuthorized);
        
        var user = GetUserById(id, userRole);
        user.IsBlocked = false;
        
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}