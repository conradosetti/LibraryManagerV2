using LibraryManager.API.Models;
using LibraryManager.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API.Controllers;


[Route("api/[controller]")]
[ApiController]

public class UserController(LibraryManagerDbContext context) : ControllerBase
{
   [HttpGet("{id:int}")]
   public async Task<IActionResult> GetUserById(int id)
   {
      var user = await context.Users
         .Include(u => u.Loans).ThenInclude(loan => loan.Book)
         .SingleOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
      if (user == null)
         return NotFound();
      var model = new
      {
         user.Name,
         user.Email,
         Books = user.Loans.Select(l => new
         {
            BookName = l.Book.Title,
            BorrowedAt = l.CreatedAt
         }).ToList()
      };
      return Ok(model);
   }
   
   [HttpPost]
   public async Task<IActionResult> CreateUser(CreateUserInputModel model)
   {
      var user = model.ToEntity();
      await context.Users.AddAsync(user);
      await context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
   }
}