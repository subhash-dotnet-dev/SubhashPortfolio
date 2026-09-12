using SubhashPortfolio.Domain.Common;
using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.Domain.Entities;

/// <summary>
/// A message submitted via the public contact form.
/// </summary>
public class ContactMessage : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public ContactStatus Status { get; set; } = ContactStatus.New;
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime? RepliedAt { get; set; }
}