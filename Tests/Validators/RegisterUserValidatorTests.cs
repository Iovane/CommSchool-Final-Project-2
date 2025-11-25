using CommSchool_Final_Project_2.DTOs;
using CommSchool_Final_Project_2.Validators;
using FluentAssertions;

namespace Tests.Validators;

public class RegisterUserValidatorTests
{
    private readonly RegisterUserValidator _validator = new();
    
    [Fact]
    public void Validate_FirstnameEmpty_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "",
            Lastname = null,
            Username = null,
            Password = null,
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Firstname is required");
    }
    
    [Fact]
    public void Validate_FirstnameNull_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = null,
            Lastname = null,
            Username = null,
            Password = null,
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Firstname is required");
    }

    [Fact]
    public void Validate_FirstnameTooShort_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "A",
            Lastname = null,
            Username = null,
            Password = null,
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Firstname must be at least 2 characters");
    }

    [Fact]
    public void Validate_FirstnameTooLong_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "A".PadRight(51, 'A'),
            Lastname = null,
            Username = null,
            Password = null,
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Firstname cannot exceed 50 characters");
    } 
    
    [Fact]
    public void Validate_LastnameEmpty_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "",
            Username = null,
            Password = null,
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Lastname is required");
    } 
    
    [Fact]
    public void Validate_LastnameNull_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = null,
            Username = null,
            Password = null,
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Lastname is required");
    }

    [Fact]
    public void Validate_LastnameTooShort_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "A",
            Username = null,
            Password = null,
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Lastname must be at least 2 characters");
    }

    [Fact]
    public void Validate_LastnameTooLong_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "A".PadRight(51, 'A'),
            Username = null,
            Password = null,
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Lastname cannot exceed 50 characters");
    } 
    
    [Fact]
    public void Validate_UsernameEmpty_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "",
            Password = null,
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Username is required");
    }
    
    [Fact]
    public void Validate_UsernameNull_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = null,
            Password = null,
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Username is required");
    }

    [Fact]
    public void Validate_UsernameTooShort_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "A",
            Password = null,
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Username must be at least 3 characters");
    }

    [Fact]
    public void Validate_UsernameTooLong_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "A".PadRight(13, 'A'),
            Password = null,
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Username cannot exceed 12 characters");
    }
    
    [Fact]
    public void Validate_PasswordEmpty_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "AAA",
            Password = "",
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Password is required");
    }
    
    [Fact]
    public void Validate_PasswordNull_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "AAA",
            Password = null,
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Password is required");
    }

    [Fact]
    public void Validate_PasswordTooShort_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "AAA",
            Password = "A",
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Password must be at least 8 characters");
    }

    [Fact]
    public void Validate_PasswordTooLong_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "AAA",
            Password = "A".PadRight(17, 'A'),
            Age = 0,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Password cannot exceed 16 characters");
    }

    [Fact]
    public void Validate_AgeNegative_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "AAA",
            Password = "A".PadRight(15, 'A'),
            Age = -1,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Age must be at least 18");
    }

    [Fact]
    public void Validate_AgeTooBig_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "AAA",
            Password = "A".PadRight(15, 'A'),
            Age = 120,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Age must be less than or equal to 100");
    }

    [Fact]
    public void Validate_MailEmpty_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "AAA",
            Password = "A".PadRight(15, 'A'),
            Age = 18,
            Mail = "",
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Email is required");
    }
    
    [Fact]
    public void Validate_MailNull_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "AAA",
            Password = "A".PadRight(15, 'A'),
            Age = 18,
            Mail = null,
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Email is required");
    }

    [Fact]
    public void Validate_MailWrongFormatting_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "AAA",
            Password = "A".PadRight(15, 'A'),
            Age = 18,
            Mail = "null",
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Invalid email format");
    }
    
    [Fact]
    public void Validate_MailTooLong_ShouldFail()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "AAA",
            Password = "A".PadRight(15, 'A'),
            Age = 18,
            Mail = "A".PadRight(101, 'A') + "@gmail.com",
            MonthlyIncome = 0
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Email cannot exceed 100 characters");
    }

    [Fact]
    public void Validate_IncomeNegative_ShouldPass()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "AAA",
            Password = "A".PadRight(15, 'A'),
            Age = 18,
            Mail = "test@gmail.com",
            MonthlyIncome = -1
        };
        var result = _validator.Validate(dto); 
        
        result.IsValid.Should().BeFalse();
        result.Errors.First().ErrorMessage.Should().BeEquivalentTo("Monthly income cannot be negative");
    }

    [Fact]
    public void Validate_ValidData_ShouldPass()
    {
        var dto = new RegisterUserDto
        {
            Firstname = "AA",
            Lastname = "AA",
            Username = "AAA",
            Password = "A".PadRight(15, 'A'),
            Age = 18,
            Mail = "test@gmail.com",
            MonthlyIncome = 1
        };
        var result = _validator.Validate(dto);
        
        result.IsValid.Should().BeTrue();
    }
    
}