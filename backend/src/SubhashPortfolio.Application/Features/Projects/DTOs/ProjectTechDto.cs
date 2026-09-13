namespace SubhashPortfolio.Application.Features.Projects.DTOs;

public class ProjectTechDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}