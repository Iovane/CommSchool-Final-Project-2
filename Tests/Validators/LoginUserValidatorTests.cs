using CommSchool_Final_Project_2.DTOs;
using CommSchool_Final_Project_2.Validators;
using FluentAssertions;

namespace Tests.Validators;

public class LoginUserValidatorTests
{
    private readonly LoginUserValidator _validator = new();

    [Fact]
    public void Validate_MissingUsername_ShouldFail()
    {
        var dto = new LoginUserDto { Username = "", Password = "Password123" };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Username is required");
    }
    
    [Fact]
    public void Validate_MissingPassword_ShouldFail()
    {
        var dto = new LoginUserDto { Username = "testuser", Password = "" };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Password is required");
    }
    
    [Fact]
    public void Validate_ValidData_ShouldPass()
    {
        var dto = new LoginUserDto { Username = "testuser", Password = "Password123" };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeTrue();
    }

}