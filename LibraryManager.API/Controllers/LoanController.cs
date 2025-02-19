using LibraryManager.API.Entities;
using LibraryManager.API.Models;
using LibraryManager.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class LoanController(LibraryManagerDbContext context) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetLoanById(int id)
    {
        var loan = await context.Loans
            .Include(l=>l.Book)
            .Include(l=>l.User)
            .SingleOrDefaultAsync(l => l.Id == id && !l.IsDeleted);
        if (loan == null)
            return NotFound();
        var model = SingleLoanViewModel.FromEntity(loan);
        return Ok(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllLoans(string search = "")
    {
        var loans = await context.Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .Where
                (l => !l.IsDeleted && 
                      (search == "" || l.Book.Title.Contains(search) || l.User.Name.Contains(search)))
            .ToListAsync();
        var models = loans.Select
            (l => SingleLoanViewModel.FromEntity(l));
        return Ok(models);
    }
    [HttpPost]
    public async Task<IActionResult> CreateLoan(CreateLoanInputModel model)
    { 
        var books = await context.Books.Where(b => !b.IsDeleted && model.IdBooks.Contains(b.Id)).ToListAsync();
        if (books.Count == 0)
            return NotFound("No books found with the given id");
        if (books.Any(b => b.IsBorrowed))
        {
            return BadRequest("At least one book is borrowed");
        }
        
        var loans = model.IdBooks.Select(b=>new Loan(model.IdUser, b, model.DevolutionDate)).ToList();
        
        books.ForEach(b => b.ChangeStatus());
        context.Loans.AddRange(loans);
        await context.SaveChangesAsync();
        return Created();
    }
}