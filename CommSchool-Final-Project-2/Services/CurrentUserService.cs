using System.Security.Claims;
using CommSchool_Final_Project_2.Interfaces;

namespace CommSchool_Final_Project_2.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId => 
        Convert.ToInt32(_httpContextAccessor.HttpContext?.User.Identity?.Name);
    
    public string Role => 
        _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value!;
    
}