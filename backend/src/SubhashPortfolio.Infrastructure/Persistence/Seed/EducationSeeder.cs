using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Domain.Entities;
using SubhashPortfolio.Domain.Enums;
using SubhashPortfolio.Infrastructure.Persistence.Context;

namespace SubhashPortfolio.Infrastructure.Persistence.Seed;

public static class EducationSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        Guid profileId,
        IDateTimeService dateTimeService)
    {
        if (context.Educations.Any()) return;

        var educations = new List<Education>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Degree = "Bachelor of Computer Applications (BCA)",
                Institution = "Radha Govind University",
                Level = EducationLevel.Bachelor,
                StartYear = 2021,
                CompletionYear = 2024,
                GradeOrPercentage = "73.34%",
                DisplayOrder = 1,
                ProfileId = profileId,
                CreatedAt = dateTimeService.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Degree = "Senior Secondary — Science",
                Institution = "JAC Board",
                Board = "JAC",
                Level = EducationLevel.SeniorSecondary,
                CompletionYear = 2021,
                GradeOrPercentage = "77.02%",
                DisplayOrder = 2,
                ProfileId = profileId,
                CreatedAt = dateTimeService.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Degree = "Secondary School",
                Institution = "JAC Board",
                Board = "JAC",
                Level = EducationLevel.Secondary,
                CompletionYear = 2019,
                GradeOrPercentage = "77.04%",
                DisplayOrder = 3,
                ProfileId = profileId,
                CreatedAt = dateTimeService.UtcNow
            }
        };

        await context.Educations.AddRangeAsync(educations);
        await context.SaveChangesAsync();
    }
}