using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Books.Commands.Delete;

public class DeleteBookCommand(int id) : IRequest<ResultViewModel>
{
    public int BookId { get; set; } = id;
}