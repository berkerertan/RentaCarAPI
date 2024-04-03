using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandValidatorEmployee : AbstractValidator<EmployeeRegisterCommand>
{
    public RegisterCommandValidatorEmployee()
    {
        RuleFor(c => c.EmployeeForRegisterDto.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.EmployeeForRegisterDto.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Must(StrongPassword)
            .WithMessage(
                "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character."
            );
    }
    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}
public class RegisterCommandValidatorInstructor : AbstractValidator<InstructorRegisterCommand>
{
    public RegisterCommandValidatorInstructor()
    {
        RuleFor(c => c.InstructorForRegisterDto.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.InstructorForRegisterDto.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Must(StrongPassword)
            .WithMessage(
                "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character."
            );
    }
    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}
public class RegisterCommandValidatorApplicant : AbstractValidator<ApplicantRegisterCommand>
{
    public RegisterCommandValidatorApplicant()
    {
        RuleFor(c => c.ApplicantForRegisterDto.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.ApplicantForRegisterDto.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Must(StrongPassword)
            .WithMessage(
                "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character."
            );
    }
    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}
