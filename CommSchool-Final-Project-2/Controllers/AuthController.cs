using CommSchool_Final_Project_2.DTOs;
using CommSchool_Final_Project_2.Helpers;
using CommSchool_Final_Project_2.Interfaces;
using CommSchool_Final_Project_2.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CommSchool_Final_Project_2.Controllers;

[ApiController]
[Route("api/")]
public class AuthController : Controller
{
    private readonly IUserService _userService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly AppSettings _appSettings;

    public AuthController(IUserService userService, IRefreshTokenService refreshTokenService,
        IOptions<AppSettings> appSettings)
    {
        _userService = userService;
        _refreshTokenService = refreshTokenService;
        _appSettings = appSettings.Value;
    }

    [HttpPost("login")]
    [EndpointDescription("Login to the system")]
    public IActionResult Login([FromBody] LoginUserDto loginUser)
    {
        var user = _userService.Login(loginUser);

        var token = GenerateToken.GetToken(user, _appSettings.Secret);
        var refreshToken = _refreshTokenService.GenerateRefreshToken(user.Id);

        return Ok(
            new
            {
                token = token,
                refreshToken = refreshToken.Token
            }
        );
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var storedToken = await _refreshTokenService.GetRefreshTokenAsync(request.RefreshToken);
        if (storedToken == null || storedToken.Expires < DateTime.UtcNow)
            return Unauthorized();

        var user = _userService.GetUserById(storedToken.UserId);
        var newAccessToken = GenerateToken.GetToken(user, _appSettings.Secret);
        var newRefreshToken = _refreshTokenService.GenerateRefreshToken(user.Id);

        await _refreshTokenService.RevokeRefreshTokenAsync(request.RefreshToken);

        return Ok(
            new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken.Token
            }
        );
    }
}