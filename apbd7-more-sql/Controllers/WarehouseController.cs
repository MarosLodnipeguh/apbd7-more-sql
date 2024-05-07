using Microsoft.AspNetCore.Mvc;
using Tutorial6.Models.DTOs;
using Tutorial6.Repositories;

namespace Tutorial6.Controllers;

[Route("api/[controller]")]
[ApiController]

public class WarehouseController : ControllerBase
{
    private readonly IWarehouseRepository _warehouseRepository;
    
    public WarehouseController(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductDTO product)
    {
        var primaryKey = await _warehouseRepository.AddProduct(product);
        
        return Ok($"Product added with primary key: {PrimaryKey}");
    }
}