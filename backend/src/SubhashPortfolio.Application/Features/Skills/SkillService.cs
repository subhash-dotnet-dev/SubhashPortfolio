using SubhashPortfolio.Application.Common.Exceptions;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Skills.DTOs;
using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.Application.Features.Skills;

public class SkillService : ISkillService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeService _dateTimeService;

    public SkillService(IUnitOfWork unitOfWork, IDateTimeService dateTimeService)
    {
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }

    public async Task<IReadOnlyList<SkillDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var skills = await _unitOfWork.Skills.GetAllAsync(cancellationToken);
        return skills.OrderBy(s => s.Category).ThenBy(s => s.DisplayOrder).Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<SkillDto>> GetByCategoryAsync(SkillCategory category, CancellationToken cancellationToken = default)
    {
        var skills = await _unitOfWork.Skills.FindAsync(s => s.Category == category, cancellationToken);
        return skills.OrderBy(s => s.DisplayOrder).Select(MapToDto).ToList();
    }

    public async Task<SkillDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var skill = await _unitOfWork.Skills.GetByIdAsync(id, cancellationToken);
        return skill is null ? null : MapToDto(skill);
    }

    public async Task<SkillDto> CreateAsync(CreateSkillDto dto, CancellationToken cancellationToken = default)
    {
        var skill = new SubhashPortfolio.Domain.Entities.Skill
        {
            Name = dto.Name,
            Category = dto.Category,
            Proficiency = dto.Proficiency,
            YearsOfExperience = dto.YearsOfExperience,
            DisplayOrder = dto.DisplayOrder,
            IsVisible = dto.IsVisible,
            ProfileId = dto.ProfileId,
            CreatedAt = _dateTimeService.UtcNow
        };

        await _unitOfWork.Skills.AddAsync(skill, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(skill);
    }

    public async Task<SkillDto> UpdateAsync(Guid id, UpdateSkillDto dto, CancellationToken cancellationToken = default)
    {
        var skill = await _unitOfWork.Skills.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Skill", id);

        skill.Name = dto.Name;
        skill.Category = dto.Category;
        skill.Proficiency = dto.Proficiency;
        skill.YearsOfExperience = dto.YearsOfExperience;
        skill.DisplayOrder = dto.DisplayOrder;
        skill.IsVisible = dto.IsVisible;
        skill.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.Skills.Update(skill);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(skill);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var skill = await _unitOfWork.Skills.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Skill", id);

        skill.IsDeleted = true;
        skill.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.Skills.Update(skill);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static SkillDto MapToDto(SubhashPortfolio.Domain.Entities.Skill s) => new()
    {
        Id = s.Id,
        Name = s.Name,
        Category = s.Category,
        Proficiency = s.Proficiency,
        YearsOfExperience = s.YearsOfExperience,
        DisplayOrder = s.DisplayOrder,
        IsVisible = s.IsVisible
    };
}