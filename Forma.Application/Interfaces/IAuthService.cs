using Forma.Application.DTOs.Auth;

namespace Forma.Application.Interfaces;

public interface IAuthService
{
    Task SendOtpAsync(string email);
    Task VerifyOtpAsync(string email, string otp);
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}