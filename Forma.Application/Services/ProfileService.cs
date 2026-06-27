using Forma.Application.Exceptions;
using Forma.Application.Interfaces;
using Forma.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Forma.Application.Services;

public class ProfileService : IProfileService
{
    private readonly IUserRepository _userRepository;
    private readonly IFileStorageService _fileStorageService;

    public ProfileService(IUserRepository userRepository, IFileStorageService fileStorageService)
    {
        _userRepository = userRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> UploadProfilePictureAsync(Guid userId, IFormFile file)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new NotFoundException("User not found.");

        var imageUrl = await _fileStorageService.SaveProfilePictureAsync(file, userId);

        user.ProfilePicture = imageUrl;
        await _userRepository.SaveChangesAsync();

        return imageUrl;
    }
}