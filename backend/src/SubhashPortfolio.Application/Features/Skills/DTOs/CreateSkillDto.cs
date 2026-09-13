using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.Application.Features.Skills.DTOs;

public class CreateSkillDto
{
    public string Name { get; set; } = string.Empty;
    public SkillCategory Category { get; set; }
    public ProficiencyLevel Proficiency { get; set; }
    public int? YearsOfExperience { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsVisible { get; set; } = true;
    public Guid ProfileId { get; set; }
}