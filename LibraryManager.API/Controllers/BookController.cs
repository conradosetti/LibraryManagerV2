using LibraryManager.Application.Books.Commands.Create;
using LibraryManager.Application.Books.Commands.Delete;
using LibraryManager.Application.Books.Commands.Return;
using LibraryManager.Application.Books.Queries.Get;
using LibraryManager.Application.Books.Queries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class BookController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List(string search = "")
    {
        //var result = await service.ListBooksAsync(search);
        var result = await mediator.Send(new ListBooksQuery(search));
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        //var result = await service.GetBookByIdAsync(id);
        var result = await mediator.Send(new GetBookQuery(id));
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        //var result = await service.CreateBookAsync(model);
        var result = await mediator.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        //var createdBook = await service.GetBookByIdAsync(result.Data); // Fetch the created book
        var createdBook = await mediator.Send(new GetBookQuery(result.Data));
        if (!createdBook.IsSuccess)
            return StatusCode(500, "Book was created but could not be retrieved.");
        
        var locationUrl = Url.Action(nameof(GetById), new { id = result.Data });
        return Created(locationUrl, createdBook);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        //var result = await service.DeleteBookAsync(id);
        var result = await mediator.Send(new DeleteBookCommand(id));
        return result.IsSuccess ? NoContent() : NotFound(result.Message);
    }

    [HttpPut("{id:int}/return-loan")]
    public async Task<IActionResult> GiveBack(int id)
    {
        //var result = await service.GiveBackBookAsync(id);
        var result = await mediator.Send(new ReturnBookCommand(id));
        return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
    }
}