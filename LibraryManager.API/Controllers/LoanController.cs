using LibraryManager.Application.Loans.Commands.Create;
using LibraryManager.Application.Loans.Queries.Get;
using LibraryManager.Application.Loans.Queries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class LoanController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        //var result =  await service.GetLoanById(id);
        var result = await mediator.Send(new GetLoanQuery(id));
        if(!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> List(string search = "")
    {
        //var result = await service.ListLoans(search);
        var result = await mediator.Send(new ListLoansQuery(search));
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateLoanCommand command)
    { 
        //var result = await service.CreateLoan(model);
        var result = await mediator.Send(command);
        if(!result.IsSuccess)
            return BadRequest(result.Message);
        return Created("", result.Data.Select(id => new { id, url = Url.Action(nameof(GetById), new { id }) }));

    }
}