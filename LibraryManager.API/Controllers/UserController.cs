using LibraryManager.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;


[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
   [HttpGet("{id:int}")]
   public async Task<IActionResult> GetUserById(int id)
   {
      return Ok();
   }
   
   [HttpPost]
   public async Task<IActionResult> CreateUser(CreateUserInputModel model)
   {
      return NoContent();
   }
}