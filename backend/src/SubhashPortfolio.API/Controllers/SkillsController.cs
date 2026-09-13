using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Skills.DTOs;
using SubhashPortfolio.Domain.Enums;

namespace SubhashPortfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillsController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var skills = await _skillService.GetAllAsync(cancellationToken);
        return Ok(skills);
    }

    [HttpGet("category/{category}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByCategory(SkillCategory category, CancellationToken cancellationToken)
    {
        var skills = await _skillService.GetByCategoryAsync(category, cancellationToken);
        return Ok(skills);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var skill = await _skillService.GetByIdAsync(id, cancellationToken);
        return skill is null ? NotFound() : Ok(skill);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateSkillDto dto, CancellationToken cancellationToken)
    {
        var skill = await _skillService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = skill.Id }, skill);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSkillDto dto, CancellationToken cancellationToken)
    {
        var skill = await _skillService.UpdateAsync(id, dto, cancellationToken);
        return Ok(skill);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _skillService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}