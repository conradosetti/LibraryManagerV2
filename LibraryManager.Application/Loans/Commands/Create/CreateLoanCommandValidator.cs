using FluentValidation;

namespace LibraryManager.Application.Loans.Commands.Create;

public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
{
    public CreateLoanCommandValidator()
    {
        RuleFor(l=> l.IdBooks)
            .NotEmpty()
            .WithMessage("At least one book must be loaned.")
            .Must(ids => ids.All(id => id > 0))
            .WithMessage("Book IDs must be greater than zero.");
        RuleFor(l=> l.IdUser)
            .NotEmpty()
            .WithMessage("User must be selected.")
            .GreaterThan(0)
            .WithMessage("User ID must be greater than zero.");
        RuleFor(l=> l.DevolutionDate)
            .NotEmpty()
            .WithMessage("Devolution date must be selected.")
            .GreaterThan(DateTime.Now)
            .WithMessage("Devolution date cannot be in the past.");
    }
}