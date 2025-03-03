using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Books.Commands.Return;

public class ReturnBookCommand(int id) : IRequest<ResultViewModel>
{
    public int BookId { get; set; } = id;
}