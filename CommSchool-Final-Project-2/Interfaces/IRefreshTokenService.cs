using CommSchool_Final_Project_2.Data;

namespace CommSchool_Final_Project_2.Interfaces;

public interface IRefreshTokenService
{
    RefreshToken GenerateRefreshToken(int userId);
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
    Task RevokeRefreshTokenAsync(string token);
}
