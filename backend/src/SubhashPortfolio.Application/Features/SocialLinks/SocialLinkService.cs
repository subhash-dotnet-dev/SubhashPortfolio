using SubhashPortfolio.Application.Common.Exceptions;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.SocialLinks.DTOs;
using SubhashPortfolio.Domain.Entities;

namespace SubhashPortfolio.Application.Features.SocialLinks;

public class SocialLinkService : ISocialLinkService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeService _dateTimeService;

    public SocialLinkService(IUnitOfWork unitOfWork, IDateTimeService dateTimeService)
    {
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }

    public async Task<IReadOnlyList<SocialLinkDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var links = await _unitOfWork.SocialLinks.GetAllAsync(cancellationToken);
        return links.OrderBy(l => l.DisplayOrder).Select(MapToDto).ToList();
    }

    public async Task<SocialLinkDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var link = await _unitOfWork.SocialLinks.GetByIdAsync(id, cancellationToken);
        return link is null ? null : MapToDto(link);
    }

    public async Task<SocialLinkDto> CreateAsync(CreateSocialLinkDto dto, CancellationToken cancellationToken = default)
    {
        var link = new SocialLink
        {
            Platform = dto.Platform,
            DisplayName = dto.DisplayName,
            Url = dto.Url,
            IconClass = dto.IconClass,
            DisplayOrder = dto.DisplayOrder,
            IsVisible = dto.IsVisible,
            ProfileId = dto.ProfileId,
            CreatedAt = _dateTimeService.UtcNow
        };

        await _unitOfWork.SocialLinks.AddAsync(link, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(link);
    }

    public async Task<SocialLinkDto> UpdateAsync(Guid id, UpdateSocialLinkDto dto, CancellationToken cancellationToken = default)
    {
        var link = await _unitOfWork.SocialLinks.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(SocialLink), id);

        link.Platform = dto.Platform;
        link.DisplayName = dto.DisplayName;
        link.Url = dto.Url;
        link.IconClass = dto.IconClass;
        link.DisplayOrder = dto.DisplayOrder;
        link.IsVisible = dto.IsVisible;
        link.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.SocialLinks.Update(link);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(link);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var link = await _unitOfWork.SocialLinks.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(SocialLink), id);

        link.IsDeleted = true;
        link.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.SocialLinks.Update(link);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static SocialLinkDto MapToDto(SocialLink l) => new()
    {
        Id = l.Id,
        Platform = l.Platform,
        DisplayName = l.DisplayName,
        Url = l.Url,
        IconClass = l.IconClass,
        DisplayOrder = l.DisplayOrder,
        IsVisible = l.IsVisible
    };
}