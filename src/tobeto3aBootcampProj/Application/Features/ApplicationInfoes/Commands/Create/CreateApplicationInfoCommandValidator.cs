using FluentValidation;

namespace Application.Features.ApplicationInfoes.Commands.Create;

public class CreateApplicationInfoCommandValidator : AbstractValidator<CreateApplicationInfoCommand>
{
    public CreateApplicationInfoCommandValidator()
    {
        RuleFor(c => c.ApplicantId).NotEmpty();
        RuleFor(c => c.BootcampId).NotEmpty();
        RuleFor(c => c.ApplicationStateId).NotEmpty();
    }
}
