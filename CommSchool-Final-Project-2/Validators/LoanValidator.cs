using CommSchool_Final_Project_2.DTOs;
using FluentValidation;

namespace CommSchool_Final_Project_2.Validators;

public class LoanValidator : AbstractValidator<LoanDto>
{
    public LoanValidator()
    {
        RuleFor(l => l.Amount)
            .GreaterThan(0)
            .WithMessage("Loan amount must be greater than 0");

        RuleFor(l => l.Currency)
            .NotEmpty()
            .WithMessage("Currency is required")
            .Length(3)
            .WithMessage("Currency must be 3 characters (e.g., USD, EUR, GEL)");

        RuleFor(l => l.Term)
            .GreaterThan(0)
            .WithMessage("Loan term must be greater than 0")
            .LessThanOrEqualTo(360)
            .WithMessage("Loan term cannot exceed 360 months");
        
        RuleFor(l => l.InterestRate)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Interest rate must be greater than or equal to 0");

        RuleFor(l => l.MonthlyPayment)
            .GreaterThan(0)
            .WithMessage("Monthly payment must be greater than 0");
        
        RuleFor(l => l.Status)
            .NotNull()
            .WithMessage("Status is required")
            .IsInEnum()
            .WithMessage("Invalid status, must be one of: InProgress, Approved, Rejected");
    }
}