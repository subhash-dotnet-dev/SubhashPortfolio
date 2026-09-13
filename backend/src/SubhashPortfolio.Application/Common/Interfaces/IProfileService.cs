using SubhashPortfolio.Application.Features.Profile.DTOs;

namespace SubhashPortfolio.Application.Common.Interfaces;

public interface IProfileService
{
    Task<ProfileDto?> GetAsync(CancellationToken cancellationToken = default);
    Task<ProfileDto> CreateAsync(CreateProfileDto dto, CancellationToken cancellationToken = default);
    Task<ProfileDto> UpdateAsync(Guid id, UpdateProfileDto dto, CancellationToken cancellationToken = default);
}