using CommSchool_Final_Project_2.Data;

namespace CommSchool_Final_Project_2.Helpers;

public class LoanOriginationSystem
{
    public static Loan CalculateLoan(double monthlyIncome, LoanType loanType, int term, string? userId)
    {
        var loan = new Loan
        {
            UserId = Convert.ToInt32(userId),
            LoanType = loanType,
            Term = term,
            Currency = "GEL"
        };

        loan.InterestRate = loanType switch
        {
            LoanType.FastConsumer => 2
            ,
            LoanType.Installment => 0.7
            ,
            LoanType.Auto => 1.5
            ,
            _ => 0
        };
        
        var maxAffordablePayment = monthlyIncome * 0.4;
        var monthlyRate = loan.InterestRate / 12;
        var denominator = (monthlyRate * Math.Pow(1 + monthlyRate, term)) /
                             (Math.Pow(1 + monthlyRate, term) - 1);

        loan.Amount = maxAffordablePayment / denominator;
        loan.MonthlyPayment = loan.Amount / term;

        return loan;
    }
}