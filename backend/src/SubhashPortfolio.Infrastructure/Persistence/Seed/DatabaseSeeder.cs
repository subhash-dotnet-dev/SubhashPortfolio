using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Infrastructure.Persistence.Context;

namespace SubhashPortfolio.Infrastructure.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IDateTimeService dateTimeService)
    {
        // 1. User (Admin)
        await UserSeeder.SeedAsync(context, passwordHasher, dateTimeService);

        // 2. Profile (returns ProfileId for others)
        var profileId = await ProfileSeeder.SeedAsync(context, dateTimeService);

        // 3. Social Links
        await SocialLinkSeeder.SeedAsync(context, profileId, dateTimeService);

        // 4. Skills
        await SkillSeeder.SeedAsync(context, profileId, dateTimeService);

        // 5. Education
        await EducationSeeder.SeedAsync(context, profileId, dateTimeService);

        // 6. Experience + Responsibilities
        await ExperienceSeeder.SeedAsync(context, profileId, dateTimeService);

        // 7. Projects + Features + Techs
        await ProjectSeeder.SeedAsync(context, profileId, dateTimeService);
    }
}