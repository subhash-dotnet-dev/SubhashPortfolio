using SubhashPortfolio.Domain.Common;

namespace SubhashPortfolio.Domain.Entities;

/// <summary>
/// Personal profile information displayed on the portfolio home page.
/// </summary>
public class Profile : AuditableEntity
{
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
    public bool IsAvailableForHire { get; set; } = true;

    // Navigation
    public ICollection<SocialLink> SocialLinks { get; set; } = new List<SocialLink>();
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Education> Educations { get; set; } = new List<Education>();
}