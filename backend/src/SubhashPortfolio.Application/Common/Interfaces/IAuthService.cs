using SubhashPortfolio.Application.Features.Auth.DTOs;

namespace SubhashPortfolio.Application.Common.Interfaces;

/// <summary>
/// Authentication service: login, register, refresh tokens.
/// </summary>
public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default);
    Task<AuthResponseDto> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<bool> LogoutAsync(Guid userId, CancellationToken cancellationToken = default);
}