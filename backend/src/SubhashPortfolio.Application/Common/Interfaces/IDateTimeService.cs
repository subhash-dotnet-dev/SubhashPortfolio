namespace SubhashPortfolio.Application.Common.Interfaces;

/// <summary>
/// Abstraction for DateTime to make code testable.
/// </summary>
public interface IDateTimeService
{
    DateTime UtcNow { get; }
    DateTime Now { get; }
}