using LibraryManager.Application.Models;
using LibraryManager.Application.Services;
using LibraryManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API.Controllers;


[Route("api/[controller]")]
[ApiController]

public class UserController(LibraryManagerDbContext context, IUserService service) : ControllerBase
{
   [HttpGet("{id:int}")]
   public async Task<IActionResult> GetById(int id)
   {
      var result = await service.GetUserByIdAsync(id);
      if (!result.IsSuccess)
         return NotFound(result.Message);
      return Ok(result);
   }
   
   [HttpPost]
   public async Task<IActionResult> Create(CreateUserInputModel model)
   {
      var result = await service.CreateUserAsync(model);
      return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
   }
}