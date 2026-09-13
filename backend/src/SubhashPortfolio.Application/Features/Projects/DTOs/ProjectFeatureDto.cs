namespace SubhashPortfolio.Application.Features.Projects.DTOs;

public class ProjectFeatureDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}