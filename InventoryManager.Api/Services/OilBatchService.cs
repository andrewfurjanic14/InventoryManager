using InventoryManager.Shared.Dtos;
using InventoryManager.Shared;
using InventoryManager.Data;
using InventoryManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InventoryManager.Api.Services
{
    public class OilBatchService : IOilBatchService
    {
        private readonly InventoryDbContext _db;

        public OilBatchService(InventoryDbContext db)
        {
            _db = db;
        }

        // Expression projection: EF generates the JOINs to Oil/Provider itself, no Include() needed.
        private static readonly Expression<Func<OilBatch, OilBatchDto>> ToDto = b => new OilBatchDto
        {
            BatchId = b.BatchId,
            OilId = b.OilId,
            OilName = b.Oil.Name,
            ProviderId = b.ProviderId,
            ProviderName = b.Provider.Name,
            CountryOfOrigin = b.CountryOfOrigin,
            LotNumber = b.LotNumber,
            ArrivalDate = b.ArrivalDate,
            ArrivalWeight_Oz = b.ArrivalWeight_Oz,
            CurrentWeight_Oz = b.CurrentWeight_Oz,
            Cost_USD = b.Cost_USD,
            ExpirationDate = b.ExpirationDate,
            StorageLocation = b.StorageLocation,
            Status = b.Status,
            COAFilePath = b.COAFilePath,
            CreatedAt = b.CreatedAt,
            UpdatedAt = b.UpdatedAt
        };

        public async Task<List<OilBatchDto>> GetAllAsync(int? oilId = null, BatchStatus? status = null)
        {
            IQueryable<OilBatch> query = _db.OilBatches.AsNoTracking();

            if (oilId.HasValue)
                query = query.Where(b => b.OilId == oilId.Value);

            if (status.HasValue)
                query = query.Where(b => b.Status == status.Value);

            return await query
                .OrderByDescending(b => b.ArrivalDate)
                .Select(ToDto)
                .ToListAsync();
        }

        public async Task<OilBatchDto?> GetByIdAsync(int id)
        {
            return await _db.OilBatches
                .AsNoTracking()
                .Where(b => b.BatchId == id)
                .Select(ToDto)
                .FirstOrDefaultAsync();
        }

        public async Task<OilBatchDto> CreateAsync(CreateOilBatchDto dto)
        {
            if (!await _db.Oils.AnyAsync(o => o.OilId == dto.OilId))
                throw new InvalidOperationException($"Oil {dto.OilId} does not exist.");

            if (!await _db.Providers.AnyAsync(p => p.ProviderId == dto.ProviderId))
                throw new InvalidOperationException($"Provider {dto.ProviderId} does not exist.");

            var batch = new OilBatch
            {
                OilId = dto.OilId,
                ProviderId = dto.ProviderId,
                CountryOfOrigin = dto.CountryOfOrigin,
                LotNumber = dto.LotNumber,
                ArrivalDate = dto.ArrivalDate,
                ArrivalWeight_Oz = dto.ArrivalWeight_Oz,
                CurrentWeight_Oz = dto.ArrivalWeight_Oz, // a new batch starts full
                Cost_USD = dto.Cost_USD,
                ExpirationDate = dto.ExpirationDate,
                StorageLocation = dto.StorageLocation,
                COAFilePath = dto.COAFilePath
            };

            _db.OilBatches.Add(batch);
            await _db.SaveChangesAsync();

            return (await GetByIdAsync(batch.BatchId))!;
        }

        public async Task<bool> UpdateAsync(int id, UpdateOilBatchDto dto)
        {
            var batch = await _db.OilBatches.FindAsync(id);
            if (batch is null) return false;

            if (dto.CurrentWeight_Oz > batch.ArrivalWeight_Oz)
                throw new InvalidOperationException(
                    "Current weight cannot exceed the weight that arrived.");

            batch.CurrentWeight_Oz = dto.CurrentWeight_Oz;
            batch.Status = dto.Status;
            batch.ExpirationDate = dto.ExpirationDate;
            batch.StorageLocation = dto.StorageLocation;
            batch.COAFilePath = dto.COAFilePath;
            batch.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var batch = await _db.OilBatches.FindAsync(id);
            if (batch is null) return false;

            _db.OilBatches.Remove(batch);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
