using SubhashPortfolio.Application.Features.Experience.DTOs;

namespace SubhashPortfolio.Application.Common.Interfaces;

public interface IExperienceService
{
    Task<IReadOnlyList<ExperienceDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ExperienceDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ExperienceDto> CreateAsync(CreateExperienceDto dto, CancellationToken cancellationToken = default);
    Task<ExperienceDto> UpdateAsync(Guid id, UpdateExperienceDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}