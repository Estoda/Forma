using Microsoft.AspNetCore.Http;

namespace Forma.Application.Interfaces;

public interface IProfileService
{
    Task<string> UploadProfilePictureAsync(Guid userId, IFormFile file);
}