using Api;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ICustomerService _service;
    public ClientsController(ICustomerService service) { _service = service; }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? searchTerm)
    {
        var customers = await _service.GetAllForGridAsync(searchTerm);
        return Ok(ApiResponse<object>.Success(customers));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer = await _service.GetByIdAsync(id);
        return customer == null ? NotFound() : Ok(ApiResponse<object>.Success(customer));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUpdateCustomerDto customerDto)
    {
        var newCustomer = await _service.CreateAsync(customerDto);
        return CreatedAtAction(nameof(GetById), new { id = newCustomer.Id }, ApiResponse<object>.Success(newCustomer));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateUpdateCustomerDto customerDto)
    {
        await _service.UpdateAsync(id, customerDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
