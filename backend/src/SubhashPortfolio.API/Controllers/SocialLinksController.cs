using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.SocialLinks.DTOs;

namespace SubhashPortfolio.API.Controllers;

[ApiController]
[Route("api/social-links")]
public class SocialLinksController : ControllerBase
{
    private readonly ISocialLinkService _socialLinkService;

    public SocialLinksController(ISocialLinkService socialLinkService)
    {
        _socialLinkService = socialLinkService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var links = await _socialLinkService.GetAllAsync(cancellationToken);
        return Ok(links);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var link = await _socialLinkService.GetByIdAsync(id, cancellationToken);
        return link is null ? NotFound() : Ok(link);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateSocialLinkDto dto, CancellationToken cancellationToken)
    {
        var link = await _socialLinkService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = link.Id }, link);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSocialLinkDto dto, CancellationToken cancellationToken)
    {
        var link = await _socialLinkService.UpdateAsync(id, dto, cancellationToken);
        return Ok(link);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _socialLinkService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}