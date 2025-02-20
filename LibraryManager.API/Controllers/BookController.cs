using LibraryManager.Application.Models;
using LibraryManager.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class BookController(IBookService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(string search = "")
    {
        var result = await service.GetAllBooksAsync(search);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.GetBookByIdAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookInputModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await service.CreateBookAsync(model);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        var createdBook = await service.GetBookByIdAsync(result.Data); // Fetch the created book
        if (createdBook == null)
            return StatusCode(500, "Book was created but could not be retrieved.");
        
        var locationUrl = Url.Action(nameof(GetById), new { id = result.Data });
        return Created(locationUrl, createdBook);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await service.DeleteBookAsync(id);
        return result.IsSuccess ? NoContent() : NotFound(result.Message);
    }

    [HttpPut("{id:int}/return-loan")]
    public async Task<IActionResult> GiveBack(int id)
    {
        var result = await service.GiveBackBookAsync(id);
        return result.IsSuccess ? NoContent() : BadRequest(result.Message);
    }
}