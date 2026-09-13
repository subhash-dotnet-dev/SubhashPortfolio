using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Application.Features.Profile.DTOs;

namespace SubhashPortfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var profile = await _profileService.GetAsync(cancellationToken);
        return profile is null ? NotFound(new { message = "Profile not found." }) : Ok(profile);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateProfileDto dto, CancellationToken cancellationToken)
    {
        var profile = await _profileService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = profile.Id }, profile);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProfileDto dto, CancellationToken cancellationToken)
    {
        var profile = await _profileService.UpdateAsync(id, dto, cancellationToken);
        return Ok(profile);
    }
}