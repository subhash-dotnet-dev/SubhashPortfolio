using SubhashPortfolio.Application.Common.Exceptions;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Contact.DTOs;
using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.Application.Features.Contact;

public class ContactService : IContactService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeService _dateTimeService;

    public ContactService(IUnitOfWork unitOfWork, IDateTimeService dateTimeService)
    {
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }

    public async Task<ContactMessageDto> SubmitAsync(
        CreateContactMessageDto dto,
        string? ipAddress,
        string? userAgent,
        CancellationToken cancellationToken = default)
    {
        var message = new SubhashPortfolio.Domain.Entities.ContactMessage
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Subject = dto.Subject,
            Message = dto.Message,
            Status = ContactStatus.New,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            CreatedAt = _dateTimeService.UtcNow
        };

        await _unitOfWork.ContactMessages.AddAsync(message, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(message);
    }

    public async Task<IReadOnlyList<ContactMessageDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _unitOfWork.ContactMessages.GetAllAsync(cancellationToken);
        return items.OrderByDescending(c => c.CreatedAt).Select(MapToDto).ToList();
    }

    public async Task<ContactMessageDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var message = await _unitOfWork.ContactMessages.GetByIdAsync(id, cancellationToken);
        return message is null ? null : MapToDto(message);
    }

    public async Task<ContactMessageDto> UpdateStatusAsync(Guid id, UpdateContactMessageDto dto, CancellationToken cancellationToken = default)
    {
        var message = await _unitOfWork.ContactMessages.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("ContactMessage", id);

        message.Status = dto.Status;
        message.UpdatedAt = _dateTimeService.UtcNow;

        if (dto.Status == ContactStatus.Read && message.ReadAt is null)
        {
            message.ReadAt = _dateTimeService.UtcNow;
        }
        else if (dto.Status == ContactStatus.Replied && message.RepliedAt is null)
        {
            message.RepliedAt = _dateTimeService.UtcNow;
        }

        _unitOfWork.ContactMessages.Update(message);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(message);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var message = await _unitOfWork.ContactMessages.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("ContactMessage", id);

        message.IsDeleted = true;
        message.UpdatedAt = _dateTimeService.UtcNow;

        _unitOfWork.ContactMessages.Update(message);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static ContactMessageDto MapToDto(SubhashPortfolio.Domain.Entities.ContactMessage c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Email = c.Email,
        Phone = c.Phone,
        Subject = c.Subject,
        Message = c.Message,
        Status = c.Status,
        CreatedAt = c.CreatedAt,
        ReadAt = c.ReadAt,
        RepliedAt = c.RepliedAt
    };
}