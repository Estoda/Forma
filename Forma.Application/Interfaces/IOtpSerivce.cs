namespace Forma.Application.Interfaces;

public interface IOtpService
{
    string GenerateOtp(string email);
    bool VerifyOtp(string email, string otp);
    bool IsEmailVerified(string email);
}
