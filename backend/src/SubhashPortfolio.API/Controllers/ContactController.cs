using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Contact.DTOs;

namespace SubhashPortfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;
    private readonly ICurrentUserService _currentUserService;

    public ContactController(
        IContactService contactService,
        ICurrentUserService currentUserService)
    {
        _contactService = contactService;
        _currentUserService = currentUserService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Submit([FromBody] CreateContactMessageDto dto, CancellationToken cancellationToken)
    {
        var message = await _contactService.SubmitAsync(
            dto,
            _currentUserService.IpAddress,
            Request.Headers.UserAgent.ToString(),
            cancellationToken);

        return Ok(new
        {
            success = true,
            message = "Thank you for reaching out! I'll get back to you soon.",
            data = message
        });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var messages = await _contactService.GetAllAsync(cancellationToken);
        return Ok(messages);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var message = await _contactService.GetByIdAsync(id, cancellationToken);
        return message is null ? NotFound() : Ok(message);
    }

    [HttpPatch("{id:guid}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateContactMessageDto dto, CancellationToken cancellationToken)
    {
        var message = await _contactService.UpdateStatusAsync(id, dto, cancellationToken);
        return Ok(message);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _contactService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
