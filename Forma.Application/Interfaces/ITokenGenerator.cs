using Forma.Domain.Entities;

namespace Forma.Application.Interfaces;

public interface ITokenGenerator
{
    (string Token, DateTime Expiration) GenerateToken(User user);
}