using Forma.Application.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Forma.Infrastructure.Storage;

public class LocalFileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private const string UploadFolder = "uploads/profile-pictures";
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
    private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

    public LocalFileStorageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> SaveProfilePictureAsync(IFormFile file, Guid userId)
    {
        ValidateFile(file);

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fileName = $"{userId}{extension}"; // overwrites old picture on re-upload

        var folderPath = Path.Combine(_env.WebRootPath, UploadFolder);
        Directory.CreateDirectory(folderPath); // no-op if it already exists

        var fullPath = Path.Combine(folderPath, fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        // Relative URL the client will use to fetch the image
        return $"/{UploadFolder}/{fileName}";
    }

    private void ValidateFile(IFormFile file)
    {
        if (file is null || file.Length == 0)
            throw new ArgumentException("No file was uploaded.");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(extension))
            throw new ArgumentException("Unsupported file type. Allowed: jpg, jpeg, png, webp.");

        if (file.Length > MaxFileSizeBytes)
            throw new ArgumentException("File size exceeds the 5MB limit.");
    }
}