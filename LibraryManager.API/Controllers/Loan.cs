using LibraryManager.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class Loan : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetLoanById(int id)
    {
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetLoan()
    {
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> CreateLoan(CreateLoanInputModel model)
    { 
        return NoContent();
    }
}