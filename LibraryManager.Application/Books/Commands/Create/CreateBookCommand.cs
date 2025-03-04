using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Entities;
using MediatR;

namespace LibraryManager.Application.Books.Commands.Create;

public class CreateBookCommand : IRequest<ResultViewModel<int>>
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Isbn { get; set; }
    public DateTime PublishDate { get; set; }
    
    public Book ToEntity()=>
        new Book(Title, Author, Isbn, PublishDate);
}