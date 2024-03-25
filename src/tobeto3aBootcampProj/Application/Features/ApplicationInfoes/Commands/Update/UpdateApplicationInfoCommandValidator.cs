using FluentValidation;

namespace Application.Features.ApplicationInfoes.Commands.Update;

public class UpdateApplicationInfoCommandValidator : AbstractValidator<UpdateApplicationInfoCommand>
{
    public UpdateApplicationInfoCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ApplicantId).NotEmpty();
        RuleFor(c => c.BootcampId).NotEmpty();
        RuleFor(c => c.ApplicationStateId).NotEmpty();
    }
}
