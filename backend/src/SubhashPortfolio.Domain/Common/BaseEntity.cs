namespace SubhashPortfolio.Domain.Common;

/// <summary>
/// Base entity with a strongly-typed primary key and audit timestamps.
/// Every domain entity must derive from this class.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}