using System.ComponentModel.DataAnnotations;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.InputModels;

public class CreateUserInputModel
{
    [Required(ErrorMessage = "Name is required.")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    public User ToEntity()
    {
        return new User(Name, Email);
    }
}