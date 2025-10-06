using System;
namespace Application.DTOs;

// Representa um item vindo do formulário do frontend
public class CreateOrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}