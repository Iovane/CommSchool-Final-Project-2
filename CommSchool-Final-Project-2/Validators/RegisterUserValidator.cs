using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.DTOs;
using FluentValidation;

namespace CommSchool_Final_Project_2.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserValidator()
    {
        RuleFor(p => p.Firstname)
            .NotEmpty()
            .WithMessage("Firstname is required")
            .MinimumLength(2)
            .WithMessage("Firstname must be at least 2 characters")
            .MaximumLength(50)
            .WithMessage("Firstname cannot exceed 50 characters");

        RuleFor(p => p.Lastname)
            .NotEmpty()
            .WithMessage("Lastname is required")
            .MinimumLength(2)
            .WithMessage("Lastname must be at least 2 characters")
            .MaximumLength(50)
            .WithMessage("Lastname cannot exceed 50 characters");

        RuleFor(p => p.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .MinimumLength(3)
            .WithMessage("Username must be at least 3 characters")
            .MaximumLength(12)
            .WithMessage("Username cannot exceed 12 characters");

        RuleFor(p => p.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters")
            .MaximumLength(16)
            .WithMessage("Password cannot exceed 16 characters");

        RuleFor(p => p.Age)
            .GreaterThanOrEqualTo(18)
            .WithMessage("Age must be at least 18")
            .LessThanOrEqualTo(100)
            .WithMessage("Age must be less than or equal to 100");

        RuleFor(p => p.Mail)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format")
            .MaximumLength(100)
            .WithMessage("Email cannot exceed 100 characters");

        RuleFor(p => p.MonthlyIncome)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Monthly income cannot be negative");
    }
}