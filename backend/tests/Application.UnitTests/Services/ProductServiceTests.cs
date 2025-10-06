using System;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain;
using Moq;
using Xunit;

namespace Application.UnitTests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _mockProductRepo;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _mockProductRepo = new Mock<IProductRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _productService = new ProductService(_mockProductRepo.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task CreateAsync_WithNegativePrice_ShouldThrowArgumentException()
    {
        // Arrange (Organização)
        var productDto = new CreateUpdateProductDto { Name = "Produto Teste", Price = -10.0m };

        // Act & Assert (Ação e Verificação)
        // Verifica se o método CreateAsync lança a exceção esperada quando o preço é negativo
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _productService.CreateAsync(productDto));
        
        Assert.Equal("O preço do produto não pode ser negativo.", exception.Message);
    }

    [Fact]
    public async Task CreateAsync_WithValidData_ShouldCallRepositoryAddAndSaveChanges()
    {
        // Arrange
        var productDto = new CreateUpdateProductDto { Name = "Produto Válido", Sku = "PV-001", Price = 100.0m, StockQty = 10, IsActive = true };

        // Act
        await _productService.CreateAsync(productDto);

        // Assert
        // Verifica se o método AddAsync do repositório foi chamado exatamente uma vez
        _mockProductRepo.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);

        // Verifica se o SaveChangesAsync da Unit of Work foi chamado exatamente uma vez
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}
