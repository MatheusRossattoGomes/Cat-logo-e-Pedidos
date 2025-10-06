using Application.DTOs;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductGridDto>> GetAllForGridAsync(string? searchTerm);
        Task<Product?> GetByIdAsync(Guid id); Task AddAsync(Product product);
        void Update(Product product); void Delete(Product product); Task<int>
        SaveChangesAsync();
    }
}
