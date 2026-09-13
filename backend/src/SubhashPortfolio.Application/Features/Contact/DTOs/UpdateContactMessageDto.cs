using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.Application.Features.Contact.DTOs;

public class UpdateContactMessageDto
{
    public ContactStatus Status { get; set; }
}