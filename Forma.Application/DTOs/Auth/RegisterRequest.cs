using Forma.Domain.Enums;

namespace Forma.Application.DTOs.Auth;

public class RegisterRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public Gender? Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? ProfilePicture { get; set; } = string.Empty;
    public double Height { get; set; }
    public double Weight { get; set; }
    public Goal Goal { get; set; }
}
