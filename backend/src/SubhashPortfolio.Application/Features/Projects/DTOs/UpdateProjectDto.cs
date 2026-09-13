using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.Application.Features.Projects.DTOs;

public class UpdateProjectDto
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
    public ProjectStatus Status { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
    public List<string> Features { get; set; } = new();
    public List<string> Technologies { get; set; } = new();
}