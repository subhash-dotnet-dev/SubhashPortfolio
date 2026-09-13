using SubhashPortfolio.Application.Features.Skills.DTOs;
using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.Application.Common.Interfaces;

public interface ISkillService
{
    Task<IReadOnlyList<SkillDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SkillDto>> GetByCategoryAsync(SkillCategory category, CancellationToken cancellationToken = default);
    Task<SkillDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SkillDto> CreateAsync(CreateSkillDto dto, CancellationToken cancellationToken = default);
    Task<SkillDto> UpdateAsync(Guid id, UpdateSkillDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}