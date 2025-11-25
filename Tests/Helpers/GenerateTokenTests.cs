using System.IdentityModel.Tokens.Jwt;
using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.Helpers;
using FluentAssertions;

namespace Tests.Helpers;

public class GenerateTokenTests
{
    [Fact]
    public void GetToken_ValidUser_ReturnsJwtWithClaims()
    {
        var user = new User { Id = 1, Role = "User" };
        const string secret = "supersecretkey1234567891011121314";

        var tokenString = GenerateToken.GetToken(user, secret);

        tokenString.Should().NotBeNullOrEmpty();

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(tokenString);

        jwt.Claims.Should().Contain(c => c.Type == "unique_name" && c.Value == "1");
        jwt.Claims.Should().Contain(c => c.Type == "role" && c.Value == "User");
        jwt.ValidTo.Should().BeAfter(DateTime.UtcNow);
    }
}