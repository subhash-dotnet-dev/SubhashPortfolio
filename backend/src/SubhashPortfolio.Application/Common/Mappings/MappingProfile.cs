using AutoMapper;
using SubhashPortfolio.Application.Features.Auth.DTOs;
using SubhashPortfolio.Application.Features.Contact.DTOs;
using SubhashPortfolio.Application.Features.Education.DTOs;
using SubhashPortfolio.Application.Features.Experience.DTOs;
using SubhashPortfolio.Application.Features.Profile.DTOs;
using SubhashPortfolio.Application.Features.Projects.DTOs;
using SubhashPortfolio.Application.Features.Skills.DTOs;
using SubhashPortfolio.Application.Features.SocialLinks.DTOs;

namespace SubhashPortfolio.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Auth
        CreateMap<Domain.Entities.User, UserDto>();

        // Profile
        CreateMap<Domain.Entities.Profile, ProfileDto>();
        CreateMap<CreateProfileDto, Domain.Entities.Profile>();
        CreateMap<UpdateProfileDto, Domain.Entities.Profile>();

        // SocialLink
        CreateMap<Domain.Entities.SocialLink, SocialLinkDto>();
        CreateMap<CreateSocialLinkDto, Domain.Entities.SocialLink>();
        CreateMap<UpdateSocialLinkDto, Domain.Entities.SocialLink>();

        // Skill
        CreateMap<Domain.Entities.Skill, SkillDto>();
        CreateMap<CreateSkillDto, Domain.Entities.Skill>();
        CreateMap<UpdateSkillDto, Domain.Entities.Skill>();

        // Education
        CreateMap<Domain.Entities.Education, EducationDto>();
        CreateMap<CreateEducationDto, Domain.Entities.Education>();
        CreateMap<UpdateEducationDto, Domain.Entities.Education>();

        // Experience
        CreateMap<Domain.Entities.Experience, ExperienceDto>();
        CreateMap<Domain.Entities.ExperienceResponsibility, ExperienceResponsibilityDto>();
        CreateMap<CreateExperienceDto, Domain.Entities.Experience>();
        CreateMap<UpdateExperienceDto, Domain.Entities.Experience>();

        // Project
        CreateMap<Domain.Entities.Project, ProjectDto>();
        CreateMap<Domain.Entities.ProjectFeature, ProjectFeatureDto>();
        CreateMap<Domain.Entities.ProjectTech, ProjectTechDto>();
        CreateMap<CreateProjectDto, Domain.Entities.Project>();
        CreateMap<UpdateProjectDto, Domain.Entities.Project>();

        // Contact
        CreateMap<Domain.Entities.ContactMessage, ContactMessageDto>();
        CreateMap<CreateContactMessageDto, Domain.Entities.ContactMessage>();
        CreateMap<UpdateContactMessageDto, Domain.Entities.ContactMessage>();
    }
}