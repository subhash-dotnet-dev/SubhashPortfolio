using SubhashPortfolio.Domain.Common;
using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.Domain.Entities;

/// <summary>
/// A featured portfolio project.
/// </summary>
public class Project : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string ShortDescription { get; set; } = string.Empty;
    public string? Problem { get; set; }
    public string? Solution { get; set; }
    public string? Architecture { get; set; }
    public string? SecurityNotes { get; set; }
    public string? EngineeringChallenges { get; set; }
    public string? Results { get; set; }
    public string? GithubUrl { get; set; }
    public string? LiveDemoUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.Published;
    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }

    // FK
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; } = null!;

    // Navigation
    public ICollection<ProjectFeature> Features { get; set; } = new List<ProjectFeature>();
    public ICollection<ProjectTech> Technologies { get; set; } = new List<ProjectTech>();
}

/// <summary>
/// Feature bullet under a project.
/// </summary>
public class ProjectFeature : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }

    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}

/// <summary>
/// Technology tag used in a project.
/// </summary>
public class ProjectTech : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }

    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}