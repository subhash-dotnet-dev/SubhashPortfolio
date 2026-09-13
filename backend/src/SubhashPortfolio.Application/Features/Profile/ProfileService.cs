using SubhashPortfolio.Application.Common.Exceptions;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Profile.DTOs;
using SubhashPortfolio.Domain.Entities;

namespace SubhashPortfolio.Application.Features.Profile;

public class ProfileService : IProfileService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeService _dateTimeService;

    public ProfileService(IUnitOfWork unitOfWork, IDateTimeService dateTimeService)
    {
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }

    public async Task<ProfileDto?> GetAsync(CancellationToken cancellationToken = default)
    {
        var profiles = await _unitOfWork.Profiles.GetAllAsync(cancellationToken);
        var profile = profiles.FirstOrDefault();
        return profile is null ? null : MapToDto(profile);
    }

    public async Task<ProfileDto> CreateAsync(CreateProfileDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _unitOfWork.Profiles.GetAllAsync(cancellationToken);
        if (existing.Any())
        {
            throw new BadRequestException("A profile already exists. Use update instead.");
        }

        var profile = new SubhashPortfolio.Domain.Entities.Profile
        {
            FullName = dto.FullName,
            Title = dto.Title,
            Email = dto.Email,
            Phone = dto.Phone,
            Location = dto.Location,
            ProfileImageUrl = dto.ProfileImageUrl,
            ResumeUrl = dto.ResumeUrl,
            ShortBio = dto.ShortBio,
            ProfessionalSummary = dto.ProfessionalSummary,
            CareerDirection = dto.CareerDirection,
            IsAvailableForHire = dto.IsAvailableForHire,
            CreatedAt = _dateTimeService.UtcNow
        };

        await _unitOfWork.Profiles.AddAsync(profile, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(profile);
    }

    public async Task<ProfileDto> UpdateAsync(Guid id, UpdateProfileDto dto, CancellationToken cancellationToken = default)
    {
        var profile = await _unitOfWork.Profiles.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Profile", id);

        profile.FullName = dto.FullName;
        profile.Title = dto.Title;
        profile.Email = dto.Email;
        profile.Phone = dto.Phone;
        profile.Location = dto.Location;
        profile.ProfileImageUrl = dto.ProfileImageUrl;
        profile.ResumeUrl = dto.ResumeUrl;
        profile.ShortBio = dto.ShortBio;
        profile.ProfessionalSummary = dto.ProfessionalSummary;
        profile.CareerDirection = dto.CareerDirection;
        profile.IsAvailableForHire = dto.IsAvailableForHire;
        profile.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.Profiles.Update(profile);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(profile);
    }

    private static ProfileDto MapToDto(SubhashPortfolio.Domain.Entities.Profile p) => new()
    {
        Id = p.Id,
        FullName = p.FullName,
        Title = p.Title,
        Email = p.Email,
        Phone = p.Phone,
        Location = p.Location,
        ProfileImageUrl = p.ProfileImageUrl,
        ResumeUrl = p.ResumeUrl,
        ShortBio = p.ShortBio,
        ProfessionalSummary = p.ProfessionalSummary,
        CareerDirection = p.CareerDirection,
        IsAvailableForHire = p.IsAvailableForHire,
        CreatedAt = p.CreatedAt,
        UpdatedAt = p.UpdatedAt
    };
}