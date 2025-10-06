using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain;
namespace Application.Services;
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repo;
    public CustomerService(ICustomerRepository repo) { _repo = repo; }

    public async Task<IEnumerable<CustomerGridDto>> GetAllForGridAsync(string? searchTerm) => await _repo.GetAllForGridAsync(searchTerm);
    public async Task<Customer?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);
    
    public async Task<Customer> CreateAsync(CreateUpdateCustomerDto customerDto)
    {
        var newCustomer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = customerDto.Name,
            Email = customerDto.Email,
            Document = customerDto.Document,
            CreatedAt = DateTime.UtcNow
        };
        await _repo.AddAsync(newCustomer);
        await _repo.SaveChangesAsync();
        return newCustomer;
    }

    public async Task UpdateAsync(Guid id, CreateUpdateCustomerDto customerDto)
    {
        var customerToUpdate = await _repo.GetByIdAsync(id);
        if (customerToUpdate == null) throw new Exception("Cliente não encontrado");
        
        customerToUpdate.Name = customerDto.Name;
        customerToUpdate.Email = customerDto.Email;
        customerToUpdate.Document = customerDto.Document;
        
        _repo.Update(customerToUpdate);
        await _repo.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var customer = await _repo.GetByIdAsync(id);
        if (customer == null) throw new Exception("Cliente não encontrado");
        
        _repo.Delete(customer);
        await _repo.SaveChangesAsync();
    }
}
