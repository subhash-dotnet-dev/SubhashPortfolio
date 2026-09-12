using SubhashPortfolio.Domain.Entities;

namespace SubhashPortfolio.Application.Common.Interfaces;

/// <summary>
/// Unit of Work pattern for managing repositories and transactions.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Profile> Profiles { get; }
    IRepository<SocialLink> SocialLinks { get; }
    IRepository<Skill> Skills { get; }
    IRepository<Experience> Experiences { get; }
    IRepository<ExperienceResponsibility> ExperienceResponsibilities { get; }
    IRepository<Project> Projects { get; }
    IRepository<ProjectFeature> ProjectFeatures { get; }
    IRepository<ProjectTech> ProjectTechs { get; }
    IRepository<Education> Educations { get; }
    IRepository<ContactMessage> ContactMessages { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}