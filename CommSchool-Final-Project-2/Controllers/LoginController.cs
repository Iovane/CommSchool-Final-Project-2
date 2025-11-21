using CommSchool_Final_Project_2.DTOs;
using CommSchool_Final_Project_2.Helpers;
using CommSchool_Final_Project_2.Interfaces;
using CommSchool_Final_Project_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CommSchool_Final_Project_2.Controllers;

[ApiController]
[Route("api/")]
public class LoginController : Controller
{
    private IUserService _userService;
    private readonly AppSettings _appSettings;
    
    public LoginController(IUserService userService, IOptions<AppSettings> appSettings)
    {
        _userService = userService;
        _appSettings = appSettings.Value;
    }

    [HttpPost("login")]
    [EndpointDescription("Login to the system")]
    public IActionResult Login([FromBody] LoginUserDto loginUser)
    {
        var user = _userService.Login(loginUser);
        
        var token = GenerateToken.GetToken(user, _appSettings.Secret);
        
        return Ok(
            new
            {
                token = token
            }
        );
    }

}