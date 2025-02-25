using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Books.Queries.Get;

public class GetBookQuery(int id) : IRequest<ResultViewModel<SingleBookViewModel>>
{
    public int Id { get; set; } = id;
}