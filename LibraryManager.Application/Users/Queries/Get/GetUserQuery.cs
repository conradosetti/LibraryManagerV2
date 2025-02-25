using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Users.Queries.Get;

public class GetUserQuery(int id) : IRequest<ResultViewModel<SingleUserViewModel>>
{
    public int Id { get; set; } = id;
}