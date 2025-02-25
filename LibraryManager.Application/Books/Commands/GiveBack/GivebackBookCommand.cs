using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Books.Commands.GiveBack;

public class GivebackBookCommand(int id) : IRequest<ResultViewModel>
{
    public int Id { get; set; } = id;
}