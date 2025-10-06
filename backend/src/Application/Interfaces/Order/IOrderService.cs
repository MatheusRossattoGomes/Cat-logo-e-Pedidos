using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderGridDto>> GetAllForGridAsync(string? searchTerm);
    Task<Order> CreateAsync(CreateOrderDto orderDto, string idempotencyKey);
}