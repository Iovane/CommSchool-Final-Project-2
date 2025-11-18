using CommSchool_Final_Project_2.DTOs;
using CommSchool_Final_Project_2.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommSchool_Final_Project_2.Controllers;

[Authorize]
[ApiController]
[Route("api/")]
public class LoanController : Controller
{
    private readonly ILoanService _loanService;
    
    public LoanController(ILoanService loanService)
    {
        _loanService = loanService;
    }
    
    [HttpPost("request/loan")]
    [EndpointDescription("Request loan for a user")]
    public IActionResult RequestLoan(RequestedLoanDto requestedLoanDto)
    {
        var loan = _loanService.RequestLoan(requestedLoanDto, User.Identity?.Name);
        
        return Ok(loan);
    }
}