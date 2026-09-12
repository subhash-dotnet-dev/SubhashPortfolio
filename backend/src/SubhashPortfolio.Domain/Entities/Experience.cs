using SubhashPortfolio.Domain.Common;

namespace SubhashPortfolio.Domain.Entities;

/// <summary>
/// Professional experience (internship / training / job).
/// </summary>
public class Experience : BaseEntity
{
    public string Company { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public string? Description { get; set; }
    public string? TechEnvironment { get; set; }
    public int DisplayOrder { get; set; }

    // FK
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; } = null!;

    // Navigation
    public ICollection<ExperienceResponsibility> Responsibilities { get; set; } = new List<ExperienceResponsibility>();
}

/// <summary>
/// A single responsibility line under an experience entry.
/// </summary>
public class ExperienceResponsibility : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }

    // FK
    public Guid ExperienceId { get; set; }
    public Experience Experience { get; set; } = null!;
}