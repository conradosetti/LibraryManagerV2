using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Users.Queries.Get;

public class GetUserQueryHandler(LibraryManagerDbContext context)
: IRequestHandler<GetUserQuery, ResultViewModel<SingleUserViewModel>>
{
    public async Task<ResultViewModel<SingleUserViewModel>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Include(u => u.Loans).ThenInclude(loan => loan.Book)
            .SingleOrDefaultAsync(u => u.Id == request.Id && !u.IsDeleted, cancellationToken: cancellationToken);
        if (user == null)
            return ResultViewModel<SingleUserViewModel>.Error("User not found");
        
        var model = SingleUserViewModel.FromEntity(user);
        return ResultViewModel<SingleUserViewModel>.Success(model);
    }
}