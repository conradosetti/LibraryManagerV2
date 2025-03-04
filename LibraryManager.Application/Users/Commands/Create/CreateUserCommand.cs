using System.ComponentModel.DataAnnotations;
using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Entities;
using MediatR;

namespace LibraryManager.Application.Users.Commands.Create;

public class CreateUserCommand : IRequest<ResultViewModel<int>>
{
    public string Name { get; set; }
    public string Email { get; set; }

    public User ToEntity()
    {
        return new User(Name, Email);
    }
}