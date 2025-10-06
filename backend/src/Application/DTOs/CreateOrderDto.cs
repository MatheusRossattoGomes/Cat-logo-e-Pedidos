using System;
using System.Collections.Generic;
namespace Application.DTOs;

// Representa o pedido completo vindo do formulário do frontend
public class CreateOrderDto
{
    public Guid CustomerId { get; set; }
    public List<CreateOrderItemDto> Items { get; set; } = new();
}