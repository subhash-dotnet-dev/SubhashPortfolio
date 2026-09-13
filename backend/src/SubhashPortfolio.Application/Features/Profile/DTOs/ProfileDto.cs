namespace SubhashPortfolio.Application.Features.Profile.DTOs;

public class ProfileDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }
    public string? ResumeUrl { get; set; }
    public string ShortBio { get; set; } = string.Empty;
    public string ProfessionalSummary { get; set; } = string.Empty;
    public string CareerDirection { get; set; } = string.Empty;
    public bool IsAvailableForHire { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}