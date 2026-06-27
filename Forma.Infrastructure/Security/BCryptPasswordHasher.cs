using Forma.Application.Interfaces;
namespace Forma.Infrastructure.Security;

public class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}
