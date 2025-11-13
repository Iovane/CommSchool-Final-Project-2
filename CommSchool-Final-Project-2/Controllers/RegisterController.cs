using CommSchool_Final_Project_2.DTOs;
using CommSchool_Final_Project_2.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommSchool_Final_Project_2.Controllers;

[ApiController]
[Route("api/")]
public class RegisterController : Controller
{
    private IUserService _userService;
    
    public RegisterController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [EndpointDescription("Register user to the system")]
    public IActionResult Register(RegisterUserDto registerUser)
    {
        var registeredUser = _userService.RegisterUser(registerUser);
        
        return Ok(new
        {
            message = "User registered successfully",
            usernameForLogin = registeredUser?.Username
        });
    }
}