using LibraryManager.Application.Models;
using LibraryManager.Application.Services;
using LibraryManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class LoanController(LibraryManagerDbContext context, ILoanService service) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result =  await service.GetLoanById(id);
        if(!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(string search = "")
    {
        var result = await service.GetAllLoans(search);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateLoanInputModel model)
    { 
        var result = await service.CreateLoan(model);
        if(!result.IsSuccess)
            return BadRequest(result.Message);
        return Created("", result.Data.Select(id => new { id, url = Url.Action(nameof(GetById), new { id }) }));

    }
}