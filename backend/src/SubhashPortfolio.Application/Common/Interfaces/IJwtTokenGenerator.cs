using SubhashPortfolio.Domain.Entities;

namespace SubhashPortfolio.Application.Common.Interfaces;

/// <summary>
/// Generates JWT access tokens and refresh tokens.
/// </summary>
public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    DateTime GetAccessTokenExpiry();
}