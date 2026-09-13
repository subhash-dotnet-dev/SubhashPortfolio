using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Domain.Entities;
using SubhashPortfolio.Infrastructure.Persistence.Context;

namespace SubhashPortfolio.Infrastructure.Persistence.Seed;

public static class ProfileSeeder
{
    public static async Task<Guid> SeedAsync(
        ApplicationDbContext context,
        IDateTimeService dateTimeService)
    {
        var existing = context.Profiles.FirstOrDefault();
        if (existing is not null) return existing.Id;

        var profile = new Profile
        {
            Id = Guid.NewGuid(),
            FullName = "Subhash Yadav",
            Title = ".NET Full Stack Developer",
            Email = "subhash.dev79@gmail.com",
            Phone = "+91 9572003492",
            Location = "Ameerpet, Hyderabad, India",
            ShortBio = "Building modern web applications across frontend, backend, APIs, and databases.",
            ProfessionalSummary = "I am a .NET Full Stack Developer with hands-on experience in ASP.NET MVC application development and practical training across the Microsoft .NET ecosystem. My development foundation includes C#, .NET Core, ASP.NET Core, ASP.NET MVC, Web API, Entity Framework, ADO.NET, React.js, JavaScript, Bootstrap, and Microsoft SQL Server. During my internship at KGE Technologies, I worked on a live ASP.NET MVC application, contributing to UI improvements, form validation, debugging, navigation issues, QA-reported defects, and Git/GitHub-based development workflows.",
            CareerDirection = "C# → ASP.NET Core → Web API → React → SQL Server → Full-Stack Engineering",
            IsAvailableForHire = true,
            CreatedAt = dateTimeService.UtcNow
        };

        await context.Profiles.AddAsync(profile);
        await context.SaveChangesAsync();

        return profile.Id;
    }
}