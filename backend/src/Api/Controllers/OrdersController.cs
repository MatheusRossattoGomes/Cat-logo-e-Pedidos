using Api;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? searchTerm)
    {
        var orders = await _service.GetAllForGridAsync(searchTerm);
        return Ok(ApiResponse<object>.Success(orders));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto orderDto, [FromHeader(Name = "Idempotency-Key")] string idempotencyKey)
    {
        try
        {
            var newOrder = await _service.CreateAsync(orderDto, idempotencyKey);
            return CreatedAtAction(nameof(GetAll), new { id = newOrder.Id }, ApiResponse<object>.Success(newOrder));
        }
        catch (Exception ex)
        {
            // Em um projeto real, trataríamos exceções específicas de forma diferente
            return BadRequest(ApiResponse<object>.Error(ex.Message));
        }
    }
}
