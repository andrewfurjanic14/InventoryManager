using InventoryManager.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager.Api.Services
{
    public interface IOilService
    {
        Task<List<OilDto>> GetAllAsync();
        Task<OilDto?> GetByIdAsync(int id);
        Task<OilDto> CreateAsync(SaveOilDto dto);
        Task<bool> UpdateAsync(int id, SaveOilDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
