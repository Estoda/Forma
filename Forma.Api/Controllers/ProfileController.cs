using System.Security.Claims;
using Forma.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forma.Api.Controllers;

[ApiController]
[Route("api/profile")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpPost("upload-picture")]
    public async Task<IActionResult> UploadPicture(IFormFile file)
    {
        var userId = GetCurrentUserId();
        var imageUrl = await _profileService.UploadProfilePictureAsync(userId, file);

        return Ok(new { profilePictureUrl = imageUrl });
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            throw new UnauthorizedAccessException("Invalid or missing user identity.");

        return userId;
    }
}