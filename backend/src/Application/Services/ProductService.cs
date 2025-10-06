using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain;
namespace Application.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    public ProductService(IProductRepository repo) { _repo = repo; }

    public async Task<IEnumerable<ProductGridDto>> GetAllForGridAsync(string? searchTerm) => await _repo.GetAllForGridAsync(searchTerm);
    public async Task<Product?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

    public async Task<Product> CreateAsync(CreateUpdateProductDto productDto)
    {
        // TODO: Adicionar validações de negócio aqui (ex: SKU único, preço > 0)
        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = productDto.Name,
            Sku = productDto.Sku,
            Price = productDto.Price,
            StockQty = productDto.StockQty,
            IsActive = productDto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await _repo.AddAsync(newProduct);
        await _repo.SaveChangesAsync();
        return newProduct;
    }

    public async Task UpdateAsync(Guid id, CreateUpdateProductDto productDto)
    {
        var productToUpdate = await _repo.GetByIdAsync(id);
        if (productToUpdate == null) throw new Exception("Produto não encontrado"); // Idealmente, uma exceção customizada
        
        // TODO: Adicionar validações de negócio aqui
        productToUpdate.Name = productDto.Name;
        productToUpdate.Sku = productDto.Sku;
        productToUpdate.Price = productDto.Price;
        productToUpdate.StockQty = productDto.StockQty;
        productToUpdate.IsActive = productDto.IsActive;

        _repo.Update(productToUpdate);
        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null) throw new Exception("Produto não encontrado");
        
        // TODO: Adicionar regras de negócio (ex: não deletar se produto estiver em um pedido)
        _repo.Delete(product);
        await _repo.SaveChangesAsync();
    }
}
