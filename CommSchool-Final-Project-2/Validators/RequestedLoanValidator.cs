using CommSchool_Final_Project_2.DTOs;
using FluentValidation;

namespace CommSchool_Final_Project_2.Validators;

public class RequestedLoanValidator : AbstractValidator<RequestedLoanDto>
{
    public RequestedLoanValidator()
    {
        RuleFor(l => l.LoanType)
            .NotEmpty()
            .WithMessage("Loan type is required")
            .IsInEnum()
            .WithMessage("Invalid loan type, must be one of: FastConsumer, Auto, Installment");
        
        RuleFor(l => l.Term)
            .GreaterThan(0)
            .WithMessage("Loan term must be greater than 0")
            .LessThanOrEqualTo(360)
            .WithMessage("Loan term cannot exceed 360 months");
        
    }
}