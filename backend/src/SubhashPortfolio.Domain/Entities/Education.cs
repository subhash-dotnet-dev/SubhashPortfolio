using SubhashPortfolio.Domain.Common;
using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.Domain.Entities;

/// <summary>
/// Educational qualification.
/// </summary>
public class Education : BaseEntity
{
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string? Board { get; set; }
    public EducationLevel Level { get; set; }
    public int? StartYear { get; set; }
    public int CompletionYear { get; set; }
    public string? GradeOrPercentage { get; set; }
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }

    // FK
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; } = null!;
}