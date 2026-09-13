using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Domain.Entities;
using SubhashPortfolio.Domain.Enums;
using SubhashPortfolio.Infrastructure.Persistence.Context;

namespace SubhashPortfolio.Infrastructure.Persistence.Seed;

public static class SkillSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        Guid profileId,
        IDateTimeService dateTimeService)
    {
        if (context.Skills.Any()) return;

        var skills = new List<Skill>();
        var order = 0;

        void AddSkill(string name, SkillCategory category, ProficiencyLevel level, int? years = null)
        {
            skills.Add(new Skill
            {
                Id = Guid.NewGuid(),
                Name = name,
                Category = category,
                Proficiency = level,
                YearsOfExperience = years,
                DisplayOrder = ++order,
                IsVisible = true,
                ProfileId = profileId,
                CreatedAt = dateTimeService.UtcNow
            });
        }

        // Programming
        AddSkill("C#", SkillCategory.Programming, ProficiencyLevel.Advanced, 2);
        AddSkill("JavaScript ES6+", SkillCategory.Programming, ProficiencyLevel.Advanced, 2);
        AddSkill("SQL", SkillCategory.Programming, ProficiencyLevel.Advanced, 2);
        AddSkill("TypeScript", SkillCategory.Programming, ProficiencyLevel.Intermediate, 1);

        // Backend
        AddSkill(".NET Core", SkillCategory.Backend, ProficiencyLevel.Advanced, 2);
        AddSkill("ASP.NET Core", SkillCategory.Backend, ProficiencyLevel.Advanced, 2);
        AddSkill("ASP.NET MVC", SkillCategory.Backend, ProficiencyLevel.Advanced, 2);
        AddSkill("Web API", SkillCategory.Backend, ProficiencyLevel.Advanced, 2);
        AddSkill("Entity Framework", SkillCategory.Backend, ProficiencyLevel.Advanced, 2);
        AddSkill("EF Core", SkillCategory.Backend, ProficiencyLevel.Advanced, 2);
        AddSkill("ADO.NET", SkillCategory.Backend, ProficiencyLevel.Intermediate, 1);
        AddSkill("SignalR", SkillCategory.Backend, ProficiencyLevel.Intermediate, 1);

        // Frontend
        AddSkill("React.js", SkillCategory.Frontend, ProficiencyLevel.Advanced, 2);
        AddSkill("HTML5", SkillCategory.Frontend, ProficiencyLevel.Expert, 3);
        AddSkill("CSS3", SkillCategory.Frontend, ProficiencyLevel.Advanced, 3);
        AddSkill("Bootstrap", SkillCategory.Frontend, ProficiencyLevel.Advanced, 2);
        AddSkill("Tailwind CSS", SkillCategory.Frontend, ProficiencyLevel.Intermediate, 1);

        // Database
        AddSkill("Microsoft SQL Server", SkillCategory.Database, ProficiencyLevel.Advanced, 2);
        AddSkill("Joins", SkillCategory.Database, ProficiencyLevel.Advanced, 2);
        AddSkill("Stored Procedures", SkillCategory.Database, ProficiencyLevel.Advanced, 2);
        AddSkill("Indexing", SkillCategory.Database, ProficiencyLevel.Intermediate, 1);
        AddSkill("CRUD Operations", SkillCategory.Database, ProficiencyLevel.Expert, 2);
        AddSkill("Complex Queries", SkillCategory.Database, ProficiencyLevel.Advanced, 2);
        AddSkill("Database Optimization", SkillCategory.Database, ProficiencyLevel.Intermediate, 1);

        // Architecture
        AddSkill("MVC Architecture", SkillCategory.Architecture, ProficiencyLevel.Advanced, 2);
        AddSkill("Repository Pattern", SkillCategory.Architecture, ProficiencyLevel.Advanced, 2);
        AddSkill("Dependency Injection", SkillCategory.Architecture, ProficiencyLevel.Advanced, 2);
        AddSkill("RESTful APIs", SkillCategory.Architecture, ProficiencyLevel.Advanced, 2);
        AddSkill("JSON", SkillCategory.Architecture, ProficiencyLevel.Advanced, 3);
        AddSkill("Clean Architecture", SkillCategory.Architecture, ProficiencyLevel.Intermediate, 1);

        // Security & Quality
        AddSkill("JWT Authentication", SkillCategory.Security, ProficiencyLevel.Advanced, 2);
        AddSkill("Role-Based Authorization", SkillCategory.Security, ProficiencyLevel.Advanced, 2);
        AddSkill("Form Validation", SkillCategory.Security, ProficiencyLevel.Advanced, 2);
        AddSkill("Exception Handling", SkillCategory.Security, ProficiencyLevel.Advanced, 2);
        AddSkill("Debugging", SkillCategory.Security, ProficiencyLevel.Advanced, 2);

        // Tools
        AddSkill("Visual Studio", SkillCategory.Tools, ProficiencyLevel.Advanced, 2);
        AddSkill("VS Code", SkillCategory.Tools, ProficiencyLevel.Advanced, 2);
        AddSkill("Git", SkillCategory.Tools, ProficiencyLevel.Advanced, 2);
        AddSkill("GitHub", SkillCategory.Tools, ProficiencyLevel.Advanced, 2);
        AddSkill("Postman", SkillCategory.Tools, ProficiencyLevel.Advanced, 2);
        AddSkill("Swagger", SkillCategory.Tools, ProficiencyLevel.Advanced, 2);

        // Development Practices
        AddSkill("Version Control", SkillCategory.Practices, ProficiencyLevel.Advanced, 2);
        AddSkill("Responsive UI Development", SkillCategory.Practices, ProficiencyLevel.Advanced, 2);
        AddSkill("API Testing", SkillCategory.Practices, ProficiencyLevel.Advanced, 2);
        AddSkill("Agile/Scrum", SkillCategory.Practices, ProficiencyLevel.Intermediate, 1);
        AddSkill("Problem Solving", SkillCategory.Practices, ProficiencyLevel.Advanced, 3);

        await context.Skills.AddRangeAsync(skills);
        await context.SaveChangesAsync();
    }
}