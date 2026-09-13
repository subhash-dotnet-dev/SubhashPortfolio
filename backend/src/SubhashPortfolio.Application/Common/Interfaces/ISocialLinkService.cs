using SubhashPortfolio.Application.Features.SocialLinks.DTOs;

namespace SubhashPortfolio.Application.Common.Interfaces;

public interface ISocialLinkService
{
    Task<IReadOnlyList<SocialLinkDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SocialLinkDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SocialLinkDto> CreateAsync(CreateSocialLinkDto dto, CancellationToken cancellationToken = default);
    Task<SocialLinkDto> UpdateAsync(Guid id, UpdateSocialLinkDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}