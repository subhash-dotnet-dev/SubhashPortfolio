using SubhashPortfolio.Application.Common.Exceptions;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Experience.DTOs;

namespace SubhashPortfolio.Application.Features.Experience;

public class ExperienceService : IExperienceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeService _dateTimeService;

    public ExperienceService(IUnitOfWork unitOfWork, IDateTimeService dateTimeService)
    {
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }

    public async Task<IReadOnlyList<ExperienceDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _unitOfWork.Experiences.GetAllAsync(cancellationToken);
        var dtos = new List<ExperienceDto>();

        foreach (var exp in items.OrderByDescending(e => e.StartDate))
        {
            var responsibilities = await _unitOfWork.ExperienceResponsibilities
                .FindAsync(r => r.ExperienceId == exp.Id, cancellationToken);

            var dto = MapToDto(exp);
            dto.Responsibilities = responsibilities
                .OrderBy(r => r.DisplayOrder)
                .Select(r => new ExperienceResponsibilityDto
                {
                    Id = r.Id,
                    Description = r.Description,
                    DisplayOrder = r.DisplayOrder
                }).ToList();

            dtos.Add(dto);
        }

        return dtos;
    }

    public async Task<ExperienceDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var exp = await _unitOfWork.Experiences.GetByIdAsync(id, cancellationToken);
        if (exp is null) return null;

        var responsibilities = await _unitOfWork.ExperienceResponsibilities
            .FindAsync(r => r.ExperienceId == exp.Id, cancellationToken);

        var dto = MapToDto(exp);
        dto.Responsibilities = responsibilities
            .OrderBy(r => r.DisplayOrder)
            .Select(r => new ExperienceResponsibilityDto
            {
                Id = r.Id,
                Description = r.Description,
                DisplayOrder = r.DisplayOrder
            }).ToList();

        return dto;
    }

    public async Task<ExperienceDto> CreateAsync(CreateExperienceDto dto, CancellationToken cancellationToken = default)
    {
        var experience = new SubhashPortfolio.Domain.Entities.Experience
        {
            Company = dto.Company,
            Role = dto.Role,
            Location = dto.Location,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            IsCurrent = dto.IsCurrent,
            Description = dto.Description,
            TechEnvironment = dto.TechEnvironment,
            DisplayOrder = dto.DisplayOrder,
            ProfileId = dto.ProfileId,
            CreatedAt = _dateTimeService.UtcNow
        };

        await _unitOfWork.Experiences.AddAsync(experience, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var order = 0;
        foreach (var responsibility in dto.Responsibilities)
        {
            await _unitOfWork.ExperienceResponsibilities.AddAsync(new SubhashPortfolio.Domain.Entities.ExperienceResponsibility
            {
                Description = responsibility,
                DisplayOrder = order++,
                ExperienceId = experience.Id,
                CreatedAt = _dateTimeService.UtcNow
            }, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return (await GetByIdAsync(experience.Id, cancellationToken))!;
    }

    public async Task<ExperienceDto> UpdateAsync(Guid id, UpdateExperienceDto dto, CancellationToken cancellationToken = default)
    {
        var experience = await _unitOfWork.Experiences.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Experience", id);

        experience.Company = dto.Company;
        experience.Role = dto.Role;
        experience.Location = dto.Location;
        experience.StartDate = dto.StartDate;
        experience.EndDate = dto.EndDate;
        experience.IsCurrent = dto.IsCurrent;
        experience.Description = dto.Description;
        experience.TechEnvironment = dto.TechEnvironment;
        experience.DisplayOrder = dto.DisplayOrder;
        experience.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.Experiences.Update(experience);

        var existing = await _unitOfWork.ExperienceResponsibilities
            .FindAsync(r => r.ExperienceId == id, cancellationToken);

        foreach (var r in existing)
        {
            r.IsDeleted = true;
            r.UpdatedAt = _dateTimeService.UtcNow;
            _unitOfWork.ExperienceResponsibilities.Update(r);
        }

        var order = 0;
        foreach (var responsibility in dto.Responsibilities)
        {
            await _unitOfWork.ExperienceResponsibilities.AddAsync(new SubhashPortfolio.Domain.Entities.ExperienceResponsibility
            {
                Description = responsibility,
                DisplayOrder = order++,
                ExperienceId = id,
                CreatedAt = _dateTimeService.UtcNow
            }, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return (await GetByIdAsync(id, cancellationToken))!;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var experience = await _unitOfWork.Experiences.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Experience", id);

        experience.IsDeleted = true;
        experience.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.Experiences.Update(experience);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static ExperienceDto MapToDto(SubhashPortfolio.Domain.Entities.Experience e) => new()
    {
        Id = e.Id,
        Company = e.Company,
        Role = e.Role,
        Location = e.Location,
        StartDate = e.StartDate,
        EndDate = e.EndDate,
        IsCurrent = e.IsCurrent,
        Description = e.Description,
        TechEnvironment = e.TechEnvironment,
        DisplayOrder = e.DisplayOrder
    };
}