using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Users.Queries.Get;

public class GetUserQueryHandler(IUserRepository repository)
: IRequestHandler<GetUserQuery, ResultViewModel<SingleUserViewModel>>
{
    public async Task<ResultViewModel<SingleUserViewModel>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserDetailsByIdAsync(request.Id);
        if (user == null)
            return ResultViewModel<SingleUserViewModel>.Error("User not found");
        
        var model = SingleUserViewModel.FromEntity(user);
        return ResultViewModel<SingleUserViewModel>.Success(model);
    }
}