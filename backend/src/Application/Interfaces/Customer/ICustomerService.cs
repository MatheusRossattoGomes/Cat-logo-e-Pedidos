using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Domain;
namespace Application.Interfaces;
public interface ICustomerService
{
    Task<IEnumerable<CustomerGridDto>> GetAllForGridAsync(string? searchTerm);
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Customer> CreateAsync(CreateUpdateCustomerDto customerDto);
    Task UpdateAsync(Guid id, CreateUpdateCustomerDto customerDto);
    Task DeleteAsync(Guid id);
}