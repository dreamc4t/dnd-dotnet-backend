using Microsoft.AspNetCore.Mvc;
using DndBackend.Services;
using DndBackend.Models;
using Microsoft.AspNetCore.Cors;

namespace DndBackend.Controllers;

[Controller]
[Route("api/[controller]")]
[EnableCors("MyAllowSpecificOrigins")] // Apply the CORS policy here
public class ShopController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public ShopController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Shop>> Get()
    {
        return await _mongoDBService.GetShopsAsync();
    }

    [HttpGet("{id}")]
    public async Task<Shop> GetById(string id)
    {
        return await _mongoDBService.GetShopByIdAsync(id);
    }

    public async Task<IActionResult> Post([FromBody] Shop newShop)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdShop = await _mongoDBService.CreateShopAsync(newShop);

        return CreatedAtAction(nameof(GetById), new { id = createdShop.Id }, createdShop);
    }


}