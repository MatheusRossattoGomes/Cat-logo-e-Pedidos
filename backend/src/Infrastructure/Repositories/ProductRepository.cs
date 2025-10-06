using Application.DTOs;
using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context) { _context = context; }

    public async Task<IEnumerable<ProductGridDto>> GetAllForGridAsync(string? searchTerm)
    {
        var query = _context.Products.AsNoTracking();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(p => EF.Functions.ILike(p.Name, $"%{searchTerm}%") || EF.Functions.ILike(p.Sku, $"%{searchTerm}%"));
        }

        return await query.OrderByDescending(p => p.CreatedAt)
            .Select(p => new ProductGridDto { Id = p.Id, Name = p.Name, Sku = p.Sku, Price = p.Price })
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(System.Guid id) => await _context.Products.FindAsync(id);
    public async Task AddAsync(Product product) => await _context.Products.AddAsync(product);
    public void Update(Product product) => _context.Products.Update(product);
    public void Delete(Product product) => _context.Products.Remove(product);
    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}