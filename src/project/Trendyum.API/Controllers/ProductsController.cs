using Microsoft.AspNetCore.Mvc;
using Trendyum.Application.Interfaces.Products;
using Trendyum.Common.Models.Products;

namespace Trendyum.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] ProductListRequest request)
    {
        var products = await _productService.GetListAsync(request);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _productService.GetProductDetailAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProductRequest request)
    {
        var createdProduct = await _productService.CreateAsync(request);
        return Created("", createdProduct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}
