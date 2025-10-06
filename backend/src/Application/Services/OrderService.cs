using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepo;
    private readonly IProductRepository _productRepo;
    private readonly IUnitOfWork _unitOfWork; 
    
    private static readonly Dictionary<string, Guid> _idempotencyCache = new();

    public OrderService(IOrderRepository orderRepo, IProductRepository productRepo, IUnitOfWork unitOfWork)
    {
        _orderRepo = orderRepo;
        _productRepo = productRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrderGridDto>> GetAllForGridAsync(string? searchTerm)
    {
        return await _orderRepo.GetAllForGridAsync(searchTerm);
    }

    public async Task<Order> CreateAsync(CreateOrderDto orderDto, string idempotencyKey)
    {
        if (string.IsNullOrEmpty(idempotencyKey))
        {
            throw new ArgumentException("O header 'Idempotency-Key' é obrigatório.");
        }

        // 1. Lógica de Idempotência
        if (_idempotencyCache.TryGetValue(idempotencyKey, out var existingOrderId))
        {
            // O ideal aqui seria buscar e retornar o pedido existente, mas para simplificar, lançamos um erro.
            throw new InvalidOperationException($"A requisição com a chave '{idempotencyKey}' já foi processada.");
        }

        // 2. Lógica de Negócio (Validação de Estoque e Cálculo)
        var newOrder = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = orderDto.CustomerId,
            Status = OrderStatus.CREATED,
            CreatedAt = DateTime.UtcNow,
            TotalAmount = 0
        };

        decimal totalAmount = 0;
        foreach (var itemDto in orderDto.Items)
        {
            var product = await _productRepo.GetByIdAsync(itemDto.ProductId);
            if (product == null)
            {
                throw new Exception($"Produto com ID {itemDto.ProductId} não encontrado.");
            }

            if (product.StockQty < itemDto.Quantity)
            {
                throw new Exception($"Estoque insuficiente para o produto '{product.Name}'. Restam {product.StockQty} em estoque.");
            }

            // Baixa no estoque
            product.StockQty -= itemDto.Quantity;
            _productRepo.Update(product); // O EF Core rastreia essa mudança para a Unit of Work

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = newOrder.Id,
                ProductId = product.Id,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price,
                LineTotal = itemDto.Quantity * product.Price
            };
            
            newOrder.OrderItems.Add(orderItem);
            totalAmount += orderItem.LineTotal;
        }

        newOrder.TotalAmount = totalAmount;

        // 3. Persistência
        await _orderRepo.AddAsync(newOrder);

        // O EF Core é inteligente. Chamar SaveChangesAsync através da Unit of Work
        // salvará o novo pedido E a atualização de estoque de todos os produtos
        // em uma única transação atômica.
        await _unitOfWork.SaveChangesAsync();
        
        // Armazena a chave de idempotência APÓS o sucesso da transação
        _idempotencyCache[idempotencyKey] = newOrder.Id;

        return newOrder;
    }
}