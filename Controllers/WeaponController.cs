using Microsoft.AspNetCore.Mvc;
using DndBackend.Services;
using DndBackend.Models;

namespace DndBackend.Controllers;

[Controller]
[Route("api/[controller]")]
public class WeaponController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public WeaponController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Weapon>> Get()
    {
        return await _mongoDBService.GetWeaponAsync();
    }

    [HttpGet("{id}")]
    public async Task<Weapon> GetById(string id)
    {
        return await _mongoDBService.GetWeaponByIdAsync(id);
    }
}