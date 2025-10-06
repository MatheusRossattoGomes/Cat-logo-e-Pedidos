using Api;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    public ProductsController(IProductService service) { _service = service; }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? searchTerm)
    {
        var products = await _service.GetAllForGridAsync(searchTerm);
        return Ok(ApiResponse<object>.Success(products));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _service.GetByIdAsync(id);
        return product == null ? NotFound() : Ok(ApiResponse<object>.Success(product));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUpdateProductDto productDto)
    {
        var newProduct = await _service.CreateAsync(productDto);
        return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, ApiResponse<object>.Success(newProduct));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateUpdateProductDto productDto)
    {
        await _service.UpdateAsync(id, productDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
