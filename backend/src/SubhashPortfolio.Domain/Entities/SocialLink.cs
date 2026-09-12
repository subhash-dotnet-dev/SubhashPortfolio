using SubhashPortfolio.Domain.Common;
using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.Domain.Entities;

/// <summary>
/// Social / professional profile links (GitHub, LinkedIn, LeetCode, etc.).
/// </summary>
public class SocialLink : BaseEntity
{
    public SocialPlatform Platform { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? IconClass { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsVisible { get; set; } = true;

    // FK
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; } = null!;
}