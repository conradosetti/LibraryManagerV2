using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Infrastructure.Persistence;
using MediatR;

namespace LibraryManager.Application.Users.Commands.Create;

public class CreateUserCommandHandler(LibraryManagerDbContext context)
: IRequestHandler<CreateUserCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToEntity();
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return ResultViewModel<int>.Success(user.Id);
    }
}