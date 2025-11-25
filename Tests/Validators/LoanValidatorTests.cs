using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.DTOs;
using CommSchool_Final_Project_2.Validators;
using FluentAssertions;

namespace Tests.Validators;

public class LoanValidatorTests
{
    private readonly LoanValidator _validator = new();
    
    [Fact]
    public void Validate_AmountZero_ShouldFail()
    {
        var dto = new LoanDto
        {
            Amount = 0,
            Currency = null,
            Term = 0,
            InterestRate = 0,
            MonthlyPayment = 0,
            Status = Status.InProgress
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Loan amount must be greater than 0");
    }
    
    [Fact]
    public void Validate_CurrencyEmpty_ShouldFail()
    {
        var dto = new LoanDto
        {
            Amount = 1,
            Currency = "",
            Term = 0,
            InterestRate = 0,
            MonthlyPayment = 0,
            Status = Status.InProgress
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Currency is required");
    }
    
    [Fact]
    public void Validate_CurrencyNull_ShouldFail()
    {
        var dto = new LoanDto
        {
            Amount = 1,
            Currency = null,
            Term = 0,
            InterestRate = 0,
            MonthlyPayment = 0,
            Status = Status.InProgress
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Currency is required");
    }
        
    [Fact]
    public void Validate_CurrencyTooLong_ShouldFail()
    {
        var dto = new LoanDto
        {
            Amount = 1,
            Currency = "null",
            Term = 0,
            InterestRate = 0,
            MonthlyPayment = 0,
            Status = Status.InProgress
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Currency must be 3 characters (e.g., USD, EUR, GEL)");
    }
            
    [Fact]
    public void Validate_TermZero_ShouldFail()
    {
        var dto = new LoanDto
        {
            Amount = 1,
            Currency = "GEL",
            Term = 0,
            InterestRate = 0,
            MonthlyPayment = 0,
            Status = Status.InProgress
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Loan term must be greater than 0");
    }
                
    [Fact]
    public void Validate_TermTooLong_ShouldFail()
    {
        var dto = new LoanDto
        {
            Amount = 1,
            Currency = "GEL",
            Term = 361,
            InterestRate = 0,
            MonthlyPayment = 0,
            Status = Status.InProgress
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Loan term cannot exceed 360 months");
    }
                    
    [Fact]
    public void Validate_InterestRateNegative_ShouldFail()
    {
        var dto = new LoanDto
        {
            Amount = 1,
            Currency = "GEL",
            Term = 360,
            InterestRate = -1,
            MonthlyPayment = 0,
            Status = Status.InProgress
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Interest rate must be greater than or equal to 0");
    }
                        
    [Fact]
    public void Validate_MonthlyPaymentZero_ShouldFail()
    {
        var dto = new LoanDto
        {
            Amount = 1,
            Currency = "GEL",
            Term = 360,
            InterestRate = 1,
            MonthlyPayment = 0,
            Status = Status.InProgress
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Monthly payment must be greater than 0");
    }
                            
    [Fact]
    public void Validate_AllFieldsCorrect_ShouldFail()
    {
        var dto = new LoanDto
        {
            Amount = 1,
            Currency = "GEL",
            Term = 360,
            InterestRate = 1,
            MonthlyPayment = 1,
            Status = Status.InProgress
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeTrue();
    }
}