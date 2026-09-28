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
    public class OilService : IOilService
    {
        private readonly InventoryDbContext _db;

        public OilService(InventoryDbContext db)
        {
            _db = db;
        }

        // Expression (not a method) so EF translates it into SQL, including the SUM subquery.
        private static readonly Expression<Func<Oil, OilDto>> ToDto = o => new OilDto
        {
            OilId = o.OilId,
            Name = o.Name,
            BotanicalName = o.BotanicalName,
            ExtractionMethod = o.ExtractionMethod,
            PlantPart = o.PlantPart,
            Notes = o.Notes,
            TotalCurrentWeight_Oz = o.Batches
                .Where(b => b.Status == BatchStatus.Active)
                .Sum(b => b.CurrentWeight_Oz)
        };

        public async Task<List<OilDto>> GetAllAsync()
        {
            return await _db.Oils
                .AsNoTracking()
                .OrderBy(o => o.Name)
                .Select(ToDto)
                .ToListAsync();
        }

        public async Task<OilDto?> GetByIdAsync(int id)
        {
            return await _db.Oils
                .AsNoTracking()
                .Where(o => o.OilId == id)
                .Select(ToDto)
                .FirstOrDefaultAsync();
        }

        public async Task<OilDto> CreateAsync(SaveOilDto dto)
        {
            await EnsureNameIsUniqueAsync(dto.Name);

            var oil = new Oil
            {
                Name = dto.Name.Trim(),
                BotanicalName = dto.BotanicalName,
                ExtractionMethod = dto.ExtractionMethod,
                PlantPart = dto.PlantPart,
                Notes = dto.Notes
            };

            _db.Oils.Add(oil);
            await _db.SaveChangesAsync();

            return (await GetByIdAsync(oil.OilId))!;
        }

        public async Task<bool> UpdateAsync(int id, SaveOilDto dto)
        {
            var oil = await _db.Oils.FindAsync(id);
            if (oil is null) return false;

            await EnsureNameIsUniqueAsync(dto.Name, excludeId: id);

            oil.Name = dto.Name.Trim();
            oil.BotanicalName = dto.BotanicalName;
            oil.ExtractionMethod = dto.ExtractionMethod;
            oil.PlantPart = dto.PlantPart;
            oil.Notes = dto.Notes;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var oil = await _db.Oils.FindAsync(id);
            if (oil is null) return false;

            // FK is Restrict, so check first to give a friendly error instead of a DB exception
            var hasBatches = await _db.OilBatches.AnyAsync(b => b.OilId == id);
            if (hasBatches)
                throw new InvalidOperationException(
                    "This oil still has batches recorded against it and cannot be deleted.");

            _db.Oils.Remove(oil);
            await _db.SaveChangesAsync();
            return true;
        }

        private async Task EnsureNameIsUniqueAsync(string name, int excludeId = 0)
        {
            var trimmed = name.Trim();
            var exists = await _db.Oils.AnyAsync(o => o.Name == trimmed && o.OilId != excludeId);
            if (exists)
                throw new InvalidOperationException($"An oil named '{trimmed}' already exists.");
        }
    }
}
