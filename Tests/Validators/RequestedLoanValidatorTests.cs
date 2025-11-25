using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.DTOs;
using CommSchool_Final_Project_2.Validators;
using FluentAssertions;

namespace Tests.Validators;

public class RequestedLoanValidatorTests
{
    private readonly RequestedLoanValidator _validator = new();

    [Fact]
    public void Validate_MissingLoanType_ShouldFail()
    {
        var dto = new RequestedLoanDto
        {
            Term = 0
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Loan type is required");
    }

    [Fact]
    public void Validate_TermTooLow_ShouldFail()
    {
        var dto = new RequestedLoanDto
        {
            LoanType = LoanType.Auto,
            Term = 0
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Loan term must be greater than 0");
    }

    [Fact]
    public void Validate_TermTooBig_ShouldFail()
    {
        var dto = new RequestedLoanDto
        {
            LoanType = LoanType.Auto,
            Term = 361
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Loan term cannot exceed 360 months");
    }
    
    [Fact]
    public void Validate_ValidData_ShouldPass()
    {
        var dto = new RequestedLoanDto
        {
            LoanType = LoanType.Auto,
            Term = 360
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeTrue();
    }
    
    
}