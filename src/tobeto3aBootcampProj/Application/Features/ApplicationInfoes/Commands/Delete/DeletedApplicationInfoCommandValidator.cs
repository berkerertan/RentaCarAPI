using FluentValidation;

namespace Application.Features.ApplicationInfoes.Commands.Delete;

public class DeleteApplicationInfoCommandValidator : AbstractValidator<DeleteApplicationInfoCommand>
{
    public DeleteApplicationInfoCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
