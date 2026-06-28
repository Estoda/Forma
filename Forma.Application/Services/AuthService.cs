using Forma.Application.DTOs.Auth;
using Forma.Application.Exceptions;
using Forma.Application.Interfaces;
using Forma.Domain.Entities;
using Forma.Domain.Interfaces;

namespace Forma.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IOtpService _otpService;
    private readonly IEmailSender _emailSender;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenGenerator tokenGenerator,
        IOtpService otpService,
        IEmailSender emailSender)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
        _otpService = otpService;
        _emailSender = emailSender;
    }

    public async Task SendOtpAsync(string email)
    {
        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser is not null)
            throw new ConflictException("Email is already registered.");

        var otp = _otpService.GenerateOtp(email);

        await _emailSender.SendAsync(
            email,
            "Your Forma verification code",
            $"Your verification code is: {otp}\nIt expires in 10 minutes.");
    }

    public async Task VerifyOtpAsync(string email, string otp)
    {
        var isValid = _otpService.VerifyOtp(email, otp);
        if (!isValid)
            throw new UnauthorizedException("Invalid or expired OTP.");
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (!_otpService.IsEmailVerified(request.Email))
            throw new UnauthorizedException("Email is not verified. Please verify your email first.");

        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser is not null)
            throw new ConflictException("Email is already registered.");

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            Phone = request.Phone,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth,
            Height = request.Height,
            Weight = request.Weight,
            Goal = request.Goal,
            TrainingSince = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        var (token, expiration) = _tokenGenerator.GenerateToken(user);

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            Token = token,
            Expiration = expiration
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid email or password.");

        var (token, expiration) = _tokenGenerator.GenerateToken(user);

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            Token = token,
            Expiration = expiration
        };
    }
}