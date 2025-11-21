using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommSchool_Final_Project_2.Controllers;

[Authorize]
[ApiController]
[Route("api/")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public UserController(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
        
    }

    [HttpGet("get/userInfo")]
    [EndpointDescription("Get user info")]
    public IActionResult GetUserInfo()
    {
        var userId = _currentUserService.UserId;
        var user = _userService.GetUserInfo(userId);
        
        return Ok(user);
    }
    
    [HttpGet("get/userInfo/{userId:int}")]
    [EndpointDescription("Get user info by id")]
    public IActionResult GetUserInfoById(int userId)
    {
        var userRole = _currentUserService.Role;
        var user = _userService.GetUserById(userId, userRole);
        
        return Ok(user);
    }
    
    [HttpPost("blockUser/{userId:int}")]
    [EndpointDescription("Block user")]
    public IActionResult BlockUser(int userId)
    {
        var userRole = _currentUserService.Role;
        _userService.BlockUser(userId, userRole);
        
        return Ok(new {message = "User blocked successfully"});
    }
    
    [HttpPost("unblockUser/{userId:int}")]
    [EndpointDescription("Unblock user")]
    public IActionResult UnblockUser(int userId)
    {
        var userRole = _currentUserService.Role;
        _userService.UnblockUser(userId, userRole);
        
        return Ok(new {message = "User unblocked successfully"});
    }
}