using SubhashPortfolio.Application.Features.Education.DTOs;

namespace SubhashPortfolio.Application.Common.Interfaces;

public interface IEducationService
{
    Task<IReadOnlyList<EducationDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EducationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<EducationDto> CreateAsync(CreateEducationDto dto, CancellationToken cancellationToken = default);
    Task<EducationDto> UpdateAsync(Guid id, UpdateEducationDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}