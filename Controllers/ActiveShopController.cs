using DndBackend.Services;
using Microsoft.AspNetCore.Mvc;
using DndBackend.Models;
using Microsoft.AspNetCore.Cors;


[ApiController]
[Route("api/[controller]")]
[EnableCors("MyAllowSpecificOrigins")]
public class ActiveShopController : Controller
{
    private readonly MongoDBService _mongoDBService;
    public ActiveShopController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }


    // GET: api/ActiveShop
    [HttpGet]
    public async Task<ActionResult<ActiveShop>> GetActiveShop()
    {
        try
        {
            var activeShop = await _mongoDBService.GetActiveShopAsync();
            if (activeShop == null)
            {
                return NotFound();
            }
            return Ok(activeShop);
        }
        catch
        {
            // Log the exception
            return StatusCode(500);
        }
    }

    [HttpPost("{shopId}")]
    public async Task<IActionResult> SetActiveShop(string shopId)
    {
        try
        {
            await _mongoDBService.SetActiveShopAsync(shopId);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch
        {
            // Log the exception
            return StatusCode(500);
        }
    }


}