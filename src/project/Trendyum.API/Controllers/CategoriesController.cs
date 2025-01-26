using Microsoft.AspNetCore.Mvc;
using Trendyum.Application.Interfaces.Categories;
using Trendyum.Common.Models.Categories;

namespace Trendyum.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var categories = await _categoryService.GetListAsync();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryRequest request)
    {
        var createdCategory = await _categoryService.CreateAsync(request);
        return Created("", createdCategory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _categoryService.DeleteAsync(id);
        return NoContent();
    }
}
