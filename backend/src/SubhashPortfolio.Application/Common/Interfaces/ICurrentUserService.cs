namespace SubhashPortfolio.Application.Common.Interfaces;

/// <summary>
/// Provides info about the currently authenticated user.
/// </summary>
public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Email { get; }
    string? Role { get; }
    bool IsAuthenticated { get; }
    string? IpAddress { get; }
}