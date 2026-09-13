using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Auth;
using SubhashPortfolio.Application.Features.Contact;
using SubhashPortfolio.Application.Features.Education;
using SubhashPortfolio.Application.Features.Experience;
using SubhashPortfolio.Application.Features.Profile;
using SubhashPortfolio.Application.Features.Projects;
using SubhashPortfolio.Application.Features.Skills;
using SubhashPortfolio.Application.Features.SocialLinks;

namespace SubhashPortfolio.Application.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // AutoMapper
        services.AddAutoMapper(assembly);

        // FluentValidation
        services.AddValidatorsFromAssembly(assembly);

        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<ISocialLinkService, SocialLinkService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<IEducationService, EducationService>();
        services.AddScoped<IExperienceService, ExperienceService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IContactService, ContactService>();

        return services;
    }
}