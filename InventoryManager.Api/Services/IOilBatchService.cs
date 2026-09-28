using InventoryManager.Shared.Dtos;
using InventoryManager.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager.Api.Services
{
    public interface IOilBatchService
    {
        Task<List<OilBatchDto>> GetAllAsync(int? oilId = null, BatchStatus? status = null);
        Task<OilBatchDto?> GetByIdAsync(int id);
        Task<OilBatchDto> CreateAsync(CreateOilBatchDto dto);
        Task<bool> UpdateAsync(int id, UpdateOilBatchDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
