using SubhashPortfolio.Application.Common.Exceptions;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Projects.DTOs;

namespace SubhashPortfolio.Application.Features.Projects;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeService _dateTimeService;

    public ProjectService(IUnitOfWork unitOfWork, IDateTimeService dateTimeService)
    {
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }

    public async Task<IReadOnlyList<ProjectDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _unitOfWork.Projects.GetAllAsync(cancellationToken);
        var dtos = new List<ProjectDto>();

        foreach (var project in items.OrderBy(p => p.DisplayOrder))
        {
            dtos.Add(await LoadProjectWithChildrenAsync(project, cancellationToken));
        }

        return dtos;
    }

    public async Task<IReadOnlyList<ProjectDto>> GetFeaturedAsync(CancellationToken cancellationToken = default)
    {
        var items = await _unitOfWork.Projects
            .FindAsync(p => p.IsFeatured, cancellationToken);

        var dtos = new List<ProjectDto>();
        foreach (var project in items.OrderBy(p => p.DisplayOrder))
        {
            dtos.Add(await LoadProjectWithChildrenAsync(project, cancellationToken));
        }

        return dtos;
    }

    public async Task<ProjectDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id, cancellationToken);
        if (project is null) return null;

        return await LoadProjectWithChildrenAsync(project, cancellationToken);
    }

    public async Task<ProjectDto> CreateAsync(CreateProjectDto dto, CancellationToken cancellationToken = default)
    {
        var project = new SubhashPortfolio.Domain.Entities.Project
        {
            Title = dto.Title,
            Subtitle = dto.Subtitle,
            ShortDescription = dto.ShortDescription,
            Problem = dto.Problem,
            Solution = dto.Solution,
            Architecture = dto.Architecture,
            SecurityNotes = dto.SecurityNotes,
            EngineeringChallenges = dto.EngineeringChallenges,
            Results = dto.Results,
            GithubUrl = dto.GithubUrl,
            LiveDemoUrl = dto.LiveDemoUrl,
            ThumbnailUrl = dto.ThumbnailUrl,
            Status = dto.Status,
            DisplayOrder = dto.DisplayOrder,
            IsFeatured = dto.IsFeatured,
            ProfileId = dto.ProfileId,
            CreatedAt = _dateTimeService.UtcNow
        };

        await _unitOfWork.Projects.AddAsync(project, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var featureOrder = 0;
        foreach (var feature in dto.Features)
        {
            await _unitOfWork.ProjectFeatures.AddAsync(new SubhashPortfolio.Domain.Entities.ProjectFeature
            {
                Description = feature,
                DisplayOrder = featureOrder++,
                ProjectId = project.Id,
                CreatedAt = _dateTimeService.UtcNow
            }, cancellationToken);
        }

        var techOrder = 0;
        foreach (var tech in dto.Technologies)
        {
            await _unitOfWork.ProjectTechs.AddAsync(new SubhashPortfolio.Domain.Entities.ProjectTech
            {
                Name = tech,
                DisplayOrder = techOrder++,
                ProjectId = project.Id,
                CreatedAt = _dateTimeService.UtcNow
            }, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return (await GetByIdAsync(project.Id, cancellationToken))!;
    }

    public async Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectDto dto, CancellationToken cancellationToken = default)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Project", id);

        project.Title = dto.Title;
        project.Subtitle = dto.Subtitle;
        project.ShortDescription = dto.ShortDescription;
        project.Problem = dto.Problem;
        project.Solution = dto.Solution;
        project.Architecture = dto.Architecture;
        project.SecurityNotes = dto.SecurityNotes;
        project.EngineeringChallenges = dto.EngineeringChallenges;
        project.Results = dto.Results;
        project.GithubUrl = dto.GithubUrl;
        project.LiveDemoUrl = dto.LiveDemoUrl;
        project.ThumbnailUrl = dto.ThumbnailUrl;
        project.Status = dto.Status;
        project.DisplayOrder = dto.DisplayOrder;
        project.IsFeatured = dto.IsFeatured;
        project.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.Projects.Update(project);

        var existingFeatures = await _unitOfWork.ProjectFeatures
            .FindAsync(f => f.ProjectId == id, cancellationToken);
        foreach (var f in existingFeatures)
        {
            f.IsDeleted = true;
            f.UpdatedAt = _dateTimeService.UtcNow;
            _unitOfWork.ProjectFeatures.Update(f);
        }

        var existingTechs = await _unitOfWork.ProjectTechs
            .FindAsync(t => t.ProjectId == id, cancellationToken);
        foreach (var t in existingTechs)
        {
            t.IsDeleted = true;
            t.UpdatedAt = _dateTimeService.UtcNow;
            _unitOfWork.ProjectTechs.Update(t);
        }

        var featureOrder = 0;
        foreach (var feature in dto.Features)
        {
            await _unitOfWork.ProjectFeatures.AddAsync(new SubhashPortfolio.Domain.Entities.ProjectFeature
            {
                Description = feature,
                DisplayOrder = featureOrder++,
                ProjectId = id,
                CreatedAt = _dateTimeService.UtcNow
            }, cancellationToken);
        }

        var techOrder = 0;
        foreach (var tech in dto.Technologies)
        {
            await _unitOfWork.ProjectTechs.AddAsync(new SubhashPortfolio.Domain.Entities.ProjectTech
            {
                Name = tech,
                DisplayOrder = techOrder++,
                ProjectId = id,
                CreatedAt = _dateTimeService.UtcNow
            }, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return (await GetByIdAsync(id, cancellationToken))!;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Project", id);

        project.IsDeleted = true;
        project.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.Projects.Update(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task<ProjectDto> LoadProjectWithChildrenAsync(
        SubhashPortfolio.Domain.Entities.Project project,
        CancellationToken cancellationToken)
    {
        var features = await _unitOfWork.ProjectFeatures
            .FindAsync(f => f.ProjectId == project.Id, cancellationToken);

        var techs = await _unitOfWork.ProjectTechs
            .FindAsync(t => t.ProjectId == project.Id, cancellationToken);

        var dto = MapToDto(project);

        dto.Features = features
            .OrderBy(f => f.DisplayOrder)
            .Select(f => new ProjectFeatureDto
            {
                Id = f.Id,
                Description = f.Description,
                DisplayOrder = f.DisplayOrder
            }).ToList();

        dto.Technologies = techs
            .OrderBy(t => t.DisplayOrder)
            .Select(t => new ProjectTechDto
            {
                Id = t.Id,
                Name = t.Name,
                DisplayOrder = t.DisplayOrder
            }).ToList();

        return dto;
    }

    private static ProjectDto MapToDto(SubhashPortfolio.Domain.Entities.Project p) => new()
    {
        Id = p.Id,
        Title = p.Title,
        Subtitle = p.Subtitle,
        ShortDescription = p.ShortDescription,
        Problem = p.Problem,
        Solution = p.Solution,
        Architecture = p.Architecture,
        SecurityNotes = p.SecurityNotes,
        EngineeringChallenges = p.EngineeringChallenges,
        Results = p.Results,
        GithubUrl = p.GithubUrl,
        LiveDemoUrl = p.LiveDemoUrl,
        ThumbnailUrl = p.ThumbnailUrl,
        Status = p.Status,
        DisplayOrder = p.DisplayOrder,
        IsFeatured = p.IsFeatured
    };
}