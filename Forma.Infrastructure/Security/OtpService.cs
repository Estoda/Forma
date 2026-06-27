using Forma.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Forma.Infrastructure.Security;

public class OtpService : IOtpService
{
    private readonly IMemoryCache _cache;
    private const int OtpExpiryMinutes = 10;
    private const int VerifiedFlagExpiryMinutes = 15; 

    public OtpService(IMemoryCache cache) => _cache = cache;

    public string GenerateOtp(string email)
    {
        var otp = Random.Shared.Next(100000, 999999).ToString();

        _cache.Set(OtpKey(email), otp, TimeSpan.FromMinutes(OtpExpiryMinutes));

        return otp;
    }

    public bool VerifyOtp(string email, string otp)
    {
        if (!_cache.TryGetValue(OtpKey(email), out string? storedOtp))
            return false;

        if (storedOtp != otp)
            return false;

        _cache.Remove(OtpKey(email)); // OTP is single-use
        _cache.Set(VerifiedKey(email), true, TimeSpan.FromMinutes(VerifiedFlagExpiryMinutes));

        return true;
    }

    public bool IsEmailVerified(string email)
    {
        return _cache.TryGetValue(VerifiedKey(email), out bool verified) && verified;
    }

    private static string OtpKey(string email) => $"otp:{email.ToLowerInvariant()}";
    private static string VerifiedKey(string email) => $"otp-verified:{email.ToLowerInvariant()}";
}