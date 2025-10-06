using Application.DTOs;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerGridDto>> GetAllForGridAsync(string? searchTerm);
        Task<Customer?> GetByIdAsync(Guid id);
        Task AddAsync(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
        Task<int> SaveChangesAsync();
    }
}