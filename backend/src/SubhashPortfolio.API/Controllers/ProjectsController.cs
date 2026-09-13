using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Projects.DTOs;

namespace SubhashPortfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var projects = await _projectService.GetAllAsync(cancellationToken);
        return Ok(projects);
    }

    [HttpGet("featured")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFeatured(CancellationToken cancellationToken)
    {
        var projects = await _projectService.GetFeaturedAsync(cancellationToken);
        return Ok(projects);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var project = await _projectService.GetByIdAsync(id, cancellationToken);
        return project is null ? NotFound() : Ok(project);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateProjectDto dto, CancellationToken cancellationToken)
    {
        var project = await _projectService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectDto dto, CancellationToken cancellationToken)
    {
        var project = await _projectService.UpdateAsync(id, dto, cancellationToken);
        return Ok(project);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _projectService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}