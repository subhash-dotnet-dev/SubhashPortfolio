using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Experience.DTOs;

namespace SubhashPortfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExperienceController : ControllerBase
{
    private readonly IExperienceService _experienceService;

    public ExperienceController(IExperienceService experienceService)
    {
        _experienceService = experienceService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var items = await _experienceService.GetAllAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _experienceService.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateExperienceDto dto, CancellationToken cancellationToken)
    {
        var item = await _experienceService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExperienceDto dto, CancellationToken cancellationToken)
    {
        var item = await _experienceService.UpdateAsync(id, dto, cancellationToken);
        return Ok(item);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _experienceService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}