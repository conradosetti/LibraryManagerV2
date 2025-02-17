using LibraryManager.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class BookController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBook()
    {
        return Ok();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookInputModel inputModel)
    {
        return CreatedAtAction(nameof(GetBookById), new {id = 1}, inputModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        return NoContent();
    }

    [HttpPut("{id:int}/loan")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        return NoContent();
    }
}