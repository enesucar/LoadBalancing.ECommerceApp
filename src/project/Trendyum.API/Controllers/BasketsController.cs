using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trendyum.Application.Interfaces.Baskets;

namespace Trendyum.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BasketsController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketsController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var products = await _basketService.GetProductList();
        return Ok(products);
    }

    [HttpPost("add-item/{id}")]
    public async Task<IActionResult> AddItem(Guid id)
    {
        await _basketService.AddProductToBasket(id);
        return NoContent();
    }

    [HttpPost("remove-item/{id}")]
    public async Task<IActionResult> RemoveItem(Guid id)
    {
        await _basketService.RemoveProductFromBasket(id);
        return NoContent();
    }
}
