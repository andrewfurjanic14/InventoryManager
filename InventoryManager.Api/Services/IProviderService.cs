using InventoryManager.Api.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager.Api.Services
{
    public interface IProviderService
    {
        Task<List<ProviderDto>> GetAllAsync();
        Task<ProviderDto?> GetByIdAsync(int id);
        Task<ProviderDto> CreateAsync(SaveProviderDto dto);
        Task<bool> UpdateAsync(int id, SaveProviderDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
