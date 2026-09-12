namespace SubhashPortfolio.Domain.Common;

/// <summary>
/// Entity that tracks who created and last modified it.
/// Inherits base audit fields and extends them.
/// </summary>
public abstract class AuditableEntity : BaseEntity
{
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}