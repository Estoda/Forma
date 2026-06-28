using FluentValidation;
using Forma.Application.DTOs.Auth;

namespace Forma.Application.Validators.Auth;

public class VerifyOtpRequestValidator : AbstractValidator<VerifyOtpRequest>
{
    public VerifyOtpRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.Otp)
            .NotEmpty().WithMessage("OTP is required.")
            .Length(6).WithMessage("OTP must be 6 digits.")
            .Matches("^[0-9]{6}$").WithMessage("OTP must contain only digits.");
    }
}