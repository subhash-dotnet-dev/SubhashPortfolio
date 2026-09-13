namespace SubhashPortfolio.Application.Features.Experience.DTOs;

public class UpdateExperienceDto
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
    public List<string> Responsibilities { get; set; } = new();
}