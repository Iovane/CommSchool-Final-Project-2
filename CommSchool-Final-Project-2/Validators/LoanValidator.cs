using CommSchool_Final_Project_2.Data;
using FluentValidation;

namespace Homework_19.Validators;

public class LoanValidator : AbstractValidator<Loan>
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
    }
}