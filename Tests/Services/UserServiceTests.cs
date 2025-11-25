using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.Domain;
using CommSchool_Final_Project_2.DTOs;
using CommSchool_Final_Project_2.Exceptions;
using CommSchool_Final_Project_2.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly AppDbContext _context;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _userService = new UserService(_context);
    }

    [Fact]
    public void RegisterUser_NewUser_Success()
    {
        var registerUserDto = new RegisterUserDto
        {
            Firstname = "Test",
            Lastname = "User",
            Username = "testuser",
            Mail = "test@example.com",
            Password = "Password123",
            MonthlyIncome = 3000,
            Age = 25
        };
        var user = _userService.RegisterUser(registerUserDto);
        
        user.Should().NotBeNull();
        user.Firstname.Should().Be("Test");
        user.Lastname.Should().Be("User");
        user.Username.Should().Be("testuser");
        user.Mail.Should().Be("test@example.com");
        user.MonthlyIncome.Should().Be(3000);
        user.Age.Should().Be(25);
    }

    [Fact]
    public void RegisterUser_DuplicateUsername_ThrowsException()
    {
        _context.Users.Add(new User { Id = 1, Username = "testuser" });
        _context.SaveChanges();
    
        var registerUserDto = new RegisterUserDto
        {
            Firstname = "Test",
            Lastname = "User",
            Username = "testuser",
            Mail = "test@example.com",
            Password = "Password123",
            MonthlyIncome = 3000,
            Age = 25
        };
    
        Assert.Throws<UserAlreadyExistsException>(() => _userService.RegisterUser(registerUserDto));
    }
    
    [Fact]
    public void LoginUser_ValidCredentials_ReturnsUser()
    {
        _context.Users.Add(new User { Id = 1, Username = "testuser", Password = "$2a$11$fiYTDwPgyxzqd125Xt9tO.gerfBZ9qL0ljqizAMeuAHh5OZ2HAlo2" });
        _context.SaveChanges();

        var loginDto = new LoginUserDto
        {
           Username = "testuser",
           Password = "Password123"
        };
        
        var user = _userService.Login(loginDto);
    
        user.Should().NotBeNull();
        user.Id.Should().Be(1);
    }
    
    [Fact]
    public void LoginUser_InvalidPassword_ThrowsException()
    {
        _context.Users.Add(new User { Id = 1, Username = "testuser", Password = "$2a$11$fiYTDwPgyxzqd125Xt9tO.gerfBZ9qL0ljqizAMeuAHh5OZ2HAlo2" });
        _context.SaveChanges();
    
        var dto = new LoginUserDto { Username = "testuser", Password = "WrongPassword" };
    
        Assert.Throws<UnauthorizedAccessException>(() => _userService.Login(dto));
    }
    
    [Fact]
    public void LoginUser_UserNotFound_ThrowsException()
    {
        var dto = new LoginUserDto { Username = "testuser", Password = "Password123" };
    
        Assert.Throws<UserNotFoundException>(() => _userService.Login(dto));
    }

    [Fact]
    public void GetUserInfo_UserExists_ReturnsUser()
    {
        _context.Users.Add(new User { Id = 1, Mail = "test@example.com" });
        _context.SaveChanges();

        var user = _userService.GetUserInfo(1);

        user.Should().NotBeNull();
        user.Id.Should().Be(1);
        user.Mail.Should().Be("test@example.com");
    }

    [Fact]
    public void GetUserInfo_NotFound_ThrowsException()
    {
        Assert.Throws<UserNotFoundException>(() => _userService.GetUserInfo(99));
    }
    
    
    [Fact]
    public void GetUserById_GetUser_Success()
    {
        _context.Users.Add(new User { Id = 1, Role = UserRole.User });
        _context.SaveChanges();
        var user = _userService.GetUserById(1);
        
        user.Should().NotBeNull();
        user.Id.Should().Be(1);
        user.Role.Should().Be(UserRole.User);
    }

    [Fact]
    public void BlockUser_AccountantRole_ThrowsException()
    {
        _context.Users.Add(new User { Id = 1, Role = UserRole.User, IsBlocked = false });
        _context.SaveChanges();
        _userService.BlockUser(1);
        var user = _context.Users.FirstOrDefault(u => u.Id == 1);
        
        user.Should().NotBeNull();
        user.IsBlocked.Should().BeTrue();
    }

    [Fact]
    public void UnblockUser_AccountantRole_ThrowsException()
    {
        _context.Users.Add(new User { Id = 1, Role = UserRole.User, IsBlocked = true });
        _context.SaveChanges();
        _userService.UnblockUser(1);
        var user = _context.Users.FirstOrDefault(u => u.Id == 1);
        
        user.Should().NotBeNull();
        user.IsBlocked.Should().BeFalse();
    }
}