using LibraryManager.API.Entities;

namespace LibraryManager.API.Models;

public class CreateUserInputModel
{
    public string Name { get; set; }
    public string Email { get; set; }

    public User ToEntity()
    {
        return new User(Name, Email);
    }
}