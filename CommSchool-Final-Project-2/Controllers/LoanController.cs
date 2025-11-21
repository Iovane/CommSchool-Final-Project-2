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
        var userId = User.Identity?.Name!;
        var loan = _loanService.RequestLoan(requestedLoanDto, userId);
        
        return Ok(loan);
    }
    
    [HttpGet("get/loans")]
    [EndpointDescription("Get all loans for a user")]
    public IActionResult GetLoans()
    {
        var userId = User.Identity?.Name!;
        var loans = _loanService.GetLoans(userId);
        
        return Ok(loans);
    }
}