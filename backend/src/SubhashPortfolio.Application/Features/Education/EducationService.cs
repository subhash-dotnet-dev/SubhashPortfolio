using SubhashPortfolio.Application.Common.Exceptions;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Education.DTOs;

namespace SubhashPortfolio.Application.Features.Education;

public class EducationService : IEducationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeService _dateTimeService;

    public EducationService(IUnitOfWork unitOfWork, IDateTimeService dateTimeService)
    {
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }

    public async Task<IReadOnlyList<EducationDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _unitOfWork.Educations.GetAllAsync(cancellationToken);
        return items.OrderBy(e => e.DisplayOrder).Select(MapToDto).ToList();
    }

    public async Task<EducationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await _unitOfWork.Educations.GetByIdAsync(id, cancellationToken);
        return item is null ? null : MapToDto(item);
    }

    public async Task<EducationDto> CreateAsync(CreateEducationDto dto, CancellationToken cancellationToken = default)
    {
        var education = new SubhashPortfolio.Domain.Entities.Education
        {
            Degree = dto.Degree,
            Institution = dto.Institution,
            Board = dto.Board,
            Level = dto.Level,
            StartYear = dto.StartYear,
            CompletionYear = dto.CompletionYear,
            GradeOrPercentage = dto.GradeOrPercentage,
            Description = dto.Description,
            DisplayOrder = dto.DisplayOrder,
            ProfileId = dto.ProfileId,
            CreatedAt = _dateTimeService.UtcNow
        };

        await _unitOfWork.Educations.AddAsync(education, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(education);
    }

    public async Task<EducationDto> UpdateAsync(Guid id, UpdateEducationDto dto, CancellationToken cancellationToken = default)
    {
        var education = await _unitOfWork.Educations.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Education", id);

        education.Degree = dto.Degree;
        education.Institution = dto.Institution;
        education.Board = dto.Board;
        education.Level = dto.Level;
        education.StartYear = dto.StartYear;
        education.CompletionYear = dto.CompletionYear;
        education.GradeOrPercentage = dto.GradeOrPercentage;
        education.Description = dto.Description;
        education.DisplayOrder = dto.DisplayOrder;
        education.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.Educations.Update(education);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(education);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var education = await _unitOfWork.Educations.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Education", id);

        education.IsDeleted = true;
        education.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.Educations.Update(education);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static EducationDto MapToDto(SubhashPortfolio.Domain.Entities.Education e) => new()
    {
        Id = e.Id,
        Degree = e.Degree,
        Institution = e.Institution,
        Board = e.Board,
        Level = e.Level,
        StartYear = e.StartYear,
        CompletionYear = e.CompletionYear,
        GradeOrPercentage = e.GradeOrPercentage,
        Description = e.Description,
        DisplayOrder = e.DisplayOrder
    };
}