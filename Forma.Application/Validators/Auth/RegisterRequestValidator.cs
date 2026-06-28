using FluentValidation;
using Forma.Application.DTOs.Auth;

namespace Forma.Application.Validators.Auth;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.");
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .Must(BeAtLeast13YearsOld).WithMessage("User must be at least 13 years old.");

        RuleFor(x => x.Height)
            .GreaterThan(0).WithMessage("Height must be greater than 0.")
            .LessThan(300).WithMessage("Height value seems invalid.");

        RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than 0.")
            .LessThan(500).WithMessage("Weight value seems invalid.");

        RuleFor(x => x.Goal)
            .IsInEnum().WithMessage("Invalid goal selected.");
    }

    private bool BeAtLeast13YearsOld(DateTime dateOfBirth)
    {
        var age = DateTime.UtcNow.Year - dateOfBirth.Year;
        if (DateTime.UtcNow.DayOfYear < dateOfBirth.DayOfYear) age--;
        return age >= 13;
    }
}