using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Education.DTOs;

namespace SubhashPortfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EducationController : ControllerBase
{
    private readonly IEducationService _educationService;

    public EducationController(IEducationService educationService)
    {
        _educationService = educationService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var items = await _educationService.GetAllAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _educationService.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateEducationDto dto, CancellationToken cancellationToken)
    {
        var item = await _educationService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEducationDto dto, CancellationToken cancellationToken)
    {
        var item = await _educationService.UpdateAsync(id, dto, cancellationToken);
        return Ok(item);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _educationService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}