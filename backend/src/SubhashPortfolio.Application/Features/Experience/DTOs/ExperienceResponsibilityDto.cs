namespace SubhashPortfolio.Application.Features.Experience.DTOs;

public class ExperienceResponsibilityDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}