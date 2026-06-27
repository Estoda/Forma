using Microsoft.AspNetCore.Http;

namespace Forma.Application.Interfaces;

public interface IFileStorageService
{
    Task<string> SaveProfilePictureAsync(IFormFile file, Guid userId);
}