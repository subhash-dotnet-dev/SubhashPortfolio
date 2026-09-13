using SubhashPortfolio.Application.Features.Contact.DTOs;

namespace SubhashPortfolio.Application.Common.Interfaces;

public interface IContactService
{
    Task<ContactMessageDto> SubmitAsync(CreateContactMessageDto dto, string? ipAddress, string? userAgent, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ContactMessageDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ContactMessageDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ContactMessageDto> UpdateStatusAsync(Guid id, UpdateContactMessageDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}