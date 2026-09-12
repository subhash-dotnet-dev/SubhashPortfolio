using SubhashPortfolio.Domain.Common;
using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.Domain.Entities;

/// <summary>
/// A single technical skill (e.g. C#, ASP.NET Core, React.js).
/// </summary>
public class Skill : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public SkillCategory Category { get; set; }
    public ProficiencyLevel Proficiency { get; set; }
    public int? YearsOfExperience { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsVisible { get; set; } = true;

    // FK
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; } = null!;
}