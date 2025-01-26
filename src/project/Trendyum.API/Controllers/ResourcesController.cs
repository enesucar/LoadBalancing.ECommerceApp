 using Microsoft.AspNetCore.Mvc;
using Trendyum.Application.Interfaces.Resources;

namespace Trendyum.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ResourcesController : ControllerBase
{
    private readonly IResourceService _resourceService;

    public ResourcesController(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(IFormFile file)
    {
        var createdResource = await _resourceService.CreateAsync(file);
        return Created("", new { id = createdResource.Id, fileName = createdResource.Name });
    }
}
