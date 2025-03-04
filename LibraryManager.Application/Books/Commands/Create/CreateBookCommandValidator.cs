using FluentValidation;

namespace LibraryManager.Application.Books.Commands.Create;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(b=> b.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
        RuleFor(b=> b.Author)
            .NotEmpty().WithMessage("Author is required.")
            .MaximumLength(100).WithMessage("Author must not exceed 50 characters.");
        RuleFor(b=> b.Isbn)
            .NotEmpty().WithMessage("ISBN is required.")
            .Matches(@"^\d{13}$").WithMessage("ISBN must be exactly 13 digits.");
        RuleFor(b=> b.PublishDate)
            .NotEmpty().WithMessage("Publish date is required.")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Publish date cannot be in the future.");;
    }
}