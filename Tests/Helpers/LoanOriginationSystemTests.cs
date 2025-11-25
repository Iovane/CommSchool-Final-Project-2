using CommSchool_Final_Project_2.Data;
using CommSchool_Final_Project_2.Helpers;
using FluentAssertions;

namespace Tests.Helpers;

public class LoanOriginationSystemTests
{
    [Theory]
    [InlineData(LoanType.FastConsumer, 2)]
    [InlineData(LoanType.Installment, 0.7)]
    [InlineData(LoanType.Auto, 1.5)]
    public void CalculateLoan_AssignsCorrectInterestRate(LoanType loanType, double expectedRate)
    {
        var loan = LoanOriginationSystem.CalculateLoan(5000, loanType, 12, 1);

        loan.InterestRate.Should().Be(expectedRate);
    }

    [Fact]
    public void CalculateLoan_ComputesAmountAndMonthlyPayment()
    {
        var loan = LoanOriginationSystem.CalculateLoan(5000, LoanType.FastConsumer, 12, 1);

        loan.Amount.Should().BeGreaterThan(0);
        loan.MonthlyPayment.Should().BeGreaterThan(0);
        loan.Currency.Should().Be("GEL");
    }

    [Fact]
    public void CalculateLoan_DifferentLoanTypes_ProduceDifferentResults()
    {
        var fastConsumerLoan = LoanOriginationSystem.CalculateLoan(5000, LoanType.FastConsumer, 12, 1);
        var autoLoan = LoanOriginationSystem.CalculateLoan(5000, LoanType.Auto, 12, 1);

        fastConsumerLoan.InterestRate.Should().NotBe(autoLoan.InterestRate);
        fastConsumerLoan.MonthlyPayment.Should().NotBe(autoLoan.MonthlyPayment);
    }
}