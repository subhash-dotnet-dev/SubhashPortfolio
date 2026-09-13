using SubhashPortfolio.Application.Common.Exceptions;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Auth.DTOs;
using SubhashPortfolio.Domain.Entities;

namespace SubhashPortfolio.Application.Features.Auth;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeService _dateTimeService;

    public AuthService(
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        IDateTimeService dateTimeService)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _dateTimeService = dateTimeService;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(
            u => u.Email == dto.Email && !u.IsDeleted, cancellationToken);

        if (user is null || !_passwordHasher.Verify(dto.Password, user.PasswordHash))
        {
            return new AuthResponseDto { Success = false, Message = "Invalid email or password." };
        }

        if (!user.IsActive)
        {
            return new AuthResponseDto { Success = false, Message = "Account is deactivated." };
        }

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = _dateTimeService.UtcNow.AddDays(7);
        user.LastLoginAt = _dateTimeService.UtcNow;
        user.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Login successful.",
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = _jwtTokenGenerator.GetAccessTokenExpiry(),
            User = MapToDto(user)
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default)
    {
        var existingUser = await _unitOfWork.Users.FirstOrDefaultAsync(
            u => u.Email == dto.Email && !u.IsDeleted, cancellationToken);

        if (existingUser is not null)
        {
            throw new BadRequestException("A user with this email already exists.");
        }

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = _passwordHasher.Hash(dto.Password),
            Role = string.IsNullOrWhiteSpace(dto.Role) ? "Admin" : dto.Role,
            IsActive = true,
            CreatedAt = _dateTimeService.UtcNow
        };

        await _unitOfWork.Users.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = _dateTimeService.UtcNow.AddDays(7);
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Registration successful.",
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = _jwtTokenGenerator.GetAccessTokenExpiry(),
            User = MapToDto(user)
        };
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            throw new BadRequestException("Refresh token is required.");
        }

        var user = await _unitOfWork.Users.FirstOrDefaultAsync(
            u => u.RefreshToken == refreshToken && !u.IsDeleted, cancellationToken);

        if (user is null || user.RefreshTokenExpiry is null || user.RefreshTokenExpiry < _dateTimeService.UtcNow)
        {
            throw new UnauthorizedException("Invalid or expired refresh token.");
        }

        var newAccessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = _dateTimeService.UtcNow.AddDays(7);
        user.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Token refreshed.",
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = _jwtTokenGenerator.GetAccessTokenExpiry(),
            User = MapToDto(user)
        };
    }

    public async Task<bool> LogoutAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if (user is null) return false;

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        user.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static UserDto MapToDto(User user) => new()
    {
        Id = user.Id,
        FullName = user.FullName,
        Email = user.Email,
        Role = user.Role,
        IsActive = user.IsActive,
        LastLoginAt = user.LastLoginAt
    };
}