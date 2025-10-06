using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<OrderGridDto>> GetAllForGridAsync(string? searchTerm);
    Task AddAsync(Order order);
    Task<int> SaveChangesAsync();
}