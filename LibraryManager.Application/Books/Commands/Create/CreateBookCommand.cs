using System.ComponentModel.DataAnnotations;
using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Entities;
using MediatR;

namespace LibraryManager.Application.Books.Commands.Create;

public class CreateBookCommand : IRequest<ResultViewModel<int>>
{
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Author is required.")]
    public string Author { get; set; }
    [RegularExpression(@"^\d{13}$", ErrorMessage = "ISBN must be a 13-digit number.")]
    public string Isbn { get; set; }
    public DateTime PublishDate { get; set; }
    
    public Book ToEntity()=>
        new Book(Title, Author, Isbn, PublishDate);
}