using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Users.Commands.Create;

public class CreateUserCommandHandler( IUserRepository repository)
: IRequestHandler<CreateUserCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToEntity();
        await repository.AddUserAsync(user);
        
        return ResultViewModel<int>.Success(user.Id);
    }
}