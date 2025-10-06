using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    public OrderRepository(AppDbContext context) { _context = context; }

    public async Task<IEnumerable<OrderGridDto>> GetAllForGridAsync(string? searchTerm)
    {
        var query = _context.Orders.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(o => o.Customer != null && EF.Functions.ILike(o.Customer.Name, $"%{searchTerm}%"));
        }

        return await query
            .Include(o => o.Customer)
            .AsNoTracking()
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OrderGridDto
            {
                Id = o.Id,
                CustomerName = o.Customer != null ? o.Customer.Name : "N/A",
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                CreatedAt = o.CreatedAt
            })
            .ToListAsync();
    }
    
    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}