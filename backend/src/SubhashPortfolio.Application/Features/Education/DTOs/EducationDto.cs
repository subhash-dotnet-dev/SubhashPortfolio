using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.Application.Features.Education.DTOs;

public class EducationDto
{
    public Guid Id { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string? Board { get; set; }
    public EducationLevel Level { get; set; }
    public int? StartYear { get; set; }
    public int CompletionYear { get; set; }
    public string? GradeOrPercentage { get; set; }
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
}