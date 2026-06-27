using Forma.Application.DTOs.Auth;
using Forma.Application.Interfaces;
using Forma.Application.Exceptions;
using Forma.Domain.Entities;
using Forma.Domain.Interfaces;

namespace Forma.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenGenerator tokenGenerator)
        => (
        _userRepository,
        _passwordHasher,
        _tokenGenerator) = (userRepository, passwordHasher, tokenGenerator);

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
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
            ProfilePicture = request.ProfilePicture,
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
