using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.Application.Features.SocialLinks.DTOs;

public class UpdateSocialLinkDto
{
    public SocialPlatform Platform { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? IconClass { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsVisible { get; set; }
}