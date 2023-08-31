using Microsoft.AspNetCore.Mvc;
using DndBackend.Services;
using DndBackend.Models;

namespace DndBackend.Controllers;

[Controller]
[Route("api/[controller]")]
public class ItemController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public ItemController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Item>> Get()
    {
        return await _mongoDBService.GetAsync();
    }

    [HttpGet("{id}")]
    public async Task<Item> GetById(string id)
    {
        return await _mongoDBService.GetByIdAsync(id);
    }
}