using CommSchool_Final_Project_2.DTOs;
using FluentValidation;

namespace CommSchool_Final_Project_2.Validators;

public class LoginUserValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserValidator()
    {
        RuleFor(p => p.Username)
            .NotEmpty()
            .WithMessage("Username is required");
        
        RuleFor(p => p.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}