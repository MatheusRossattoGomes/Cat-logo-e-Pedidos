using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Domain;
namespace Application.Interfaces;
public interface IProductService
{
    Task<IEnumerable<ProductGridDto>> GetAllForGridAsync(string? searchTerm);
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product> CreateAsync(CreateUpdateProductDto productDto);
    Task UpdateAsync(Guid id, CreateUpdateProductDto productDto);
    Task DeleteAsync(Guid id);
}
