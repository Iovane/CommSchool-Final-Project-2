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
    private readonly ICurrentUserService _currentUserService;

    public LoanController(ILoanService loanService, ICurrentUserService currentUserService)
    {
        _loanService = loanService;
        _currentUserService = currentUserService;
    }
    
    [HttpPost("request/loan")]
    [EndpointDescription("Request loan for a user")]
    public IActionResult RequestLoan(RequestedLoanDto requestedLoanDto)
    {
        var userId = _currentUserService.UserId;
        var loan = _loanService.RequestLoan(requestedLoanDto, userId);
        
        return Ok(loan);
    }
    
    [HttpGet("get/loans")]
    [EndpointDescription("Get all loans for a user")]
    public IActionResult GetLoans()
    {
        var userId = _currentUserService.UserId;
        var userRole = _currentUserService.Role;
        var loans = _loanService.GetLoans(userId, userRole);
        
        return Ok(loans);
    }
    
    [HttpGet("get/loan/{loanId:int}")]
    [EndpointDescription("Get loan for a user by loan id")]
    public IActionResult GetLoan(int loanId)
    {
        var userId = _currentUserService.UserId;
        var userRole = _currentUserService.Role;
        var loan = _loanService.GetLoan(loanId, userId, userRole);
        
        return Ok(loan);
    }
    
    [HttpPut("update/loan/{loanId:int}")]
    [EndpointDescription("Update loan information  loan id")]
    public IActionResult UpdateLoan([FromBody] LoanDto updatedLoan, int loanId)
    {
        var userId = _currentUserService.UserId;
        var userRole = _currentUserService.Role;
        var loan = _loanService.UpdateLoan(updatedLoan, loanId, userId, userRole);
        
        return Ok(loan);
    }

    [HttpDelete("delete/loan/{loanId:int}")]
    [EndpointDescription("Delete loan for a user by loan id")]
    public IActionResult DeleteLoan(int loanId)
    {
        var userId = _currentUserService.UserId;
        var userRole = _currentUserService.Role;
        _loanService.DeleteLoan(loanId, userId, userRole);
        
        return Ok(new
        {
            message = "Loan deleted successfully"
        });
    }
}