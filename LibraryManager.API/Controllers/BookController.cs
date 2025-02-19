using LibraryManager.API.Entities;
using LibraryManager.API.Models;
using LibraryManager.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class BookController(LibraryManagerDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllBooks(string search = "")
    {
        var books = await context.Books
            .Where
                (b => (search == "" || b.Title.Contains(search) || b.Author.Contains(search)) && !b.IsDeleted)
            .ToListAsync();
        var model = books.Select(b => BooksViewModel.FromEntity(b)).ToList();
        return Ok(model);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await context.Books
            .Include(b => b.Loans)
            .SingleOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        if (book == null)
            return NotFound();
        var model = SingleBookViewModel.FromEntity(book);
        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody]CreateBookInputModel inputModel)
    {
        var book = inputModel.ToEntity();
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBookById), new {id = book.Id}, inputModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await context.Books
            .SingleOrDefaultAsync(b => b.Id == id || !b.IsDeleted);
        if (book == null)
            return NotFound();
        book.SetAsDeleted();
        context.Books.Update(book);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id:int}/return-loan")]
    public async Task<IActionResult> GiveBackBook(int id)
    {
        var book = await context.Books
                .Include(b => b.Loans)
                .SingleOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        if (book == null)
            return NotFound();
        if(!book.IsBorrowed)
            return BadRequest("Book is not borrowed");
        var loan = await context.Loans.SingleOrDefaultAsync(l=>l.IdBook == id && !l.IsReturned);
        book.ChangeStatus();
        context.Books.Update(book);
        if (loan.IsLate())
        {
            loan.ReturnBook();
            context.Loans.Update(loan);
            await context.SaveChangesAsync();
            return Ok("Loan is late");
        }
        loan.ReturnBook();
        context.Loans.Update(loan);
        await context.SaveChangesAsync();
        return Ok("Loan is on time");
    }
}