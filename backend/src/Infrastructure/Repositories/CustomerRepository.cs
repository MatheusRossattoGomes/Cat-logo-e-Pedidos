using Application.DTOs;
using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;
public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;
    public CustomerRepository(AppDbContext context) { _context = context; }

    public async Task<IEnumerable<CustomerGridDto>> GetAllForGridAsync(string? searchTerm)
    {
        var query = _context.Customers.AsQueryable();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(c => EF.Functions.ILike(c.Name, $"%{searchTerm}%") || EF.Functions.ILike(c.Email, $"%{searchTerm}%"));
        }
        return await query.AsNoTracking().OrderByDescending(c => c.CreatedAt)
            .Select(c => new CustomerGridDto { Id = c.Id, Name = c.Name, Email = c.Email })
            .ToListAsync();
    }
    public async Task<Customer?> GetByIdAsync(System.Guid id) => await _context.Customers.FindAsync(id);
    public async Task AddAsync(Customer customer) => await _context.Customers.AddAsync(customer);
    public void Update(Customer customer) => _context.Customers.Update(customer);
    public void Delete(Customer customer) => _context.Customers.Remove(customer);
    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}
