using FluentValidation;
using Forma.Application.DTOs.Auth;

namespace Forma.Application.Validators.Auth;

public class SendOtpRequestValidator : AbstractValidator<SendOtpRequest>
{
    public SendOtpRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");
    }
}