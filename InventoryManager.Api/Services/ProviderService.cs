using InventoryManager.Shared.Dtos;
using InventoryManager.Data;
using InventoryManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InventoryManager.Api.Services
{
    public class ProviderService : IProviderService
    {
        private readonly InventoryDbContext _db;

        public ProviderService(InventoryDbContext db)
        {
            _db = db;
        }

        private static readonly Expression<Func<Provider, ProviderDto>> ToDto = p => new ProviderDto
        {
            ProviderId = p.ProviderId,
            Name = p.Name,
            ContactEmail = p.ContactEmail,
            ContactPhone = p.ContactPhone,
            Website = p.Website
        };

        public async Task<List<ProviderDto>> GetAllAsync()
        {
            return await _db.Providers
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Select(ToDto)
                .ToListAsync();
        }

        public async Task<ProviderDto?> GetByIdAsync(int id)
        {
            return await _db.Providers
                .AsNoTracking()
                .Where(p => p.ProviderId == id)
                .Select(ToDto)
                .FirstOrDefaultAsync();
        }

        public async Task<ProviderDto> CreateAsync(SaveProviderDto dto)
        {
            var provider = new Provider
            {
                Name = dto.Name.Trim(),
                ContactEmail = dto.ContactEmail,
                ContactPhone = dto.ContactPhone,
                Website = dto.Website
            };

            _db.Providers.Add(provider);
            await _db.SaveChangesAsync();

            return (await GetByIdAsync(provider.ProviderId))!;
        }

        public async Task<bool> UpdateAsync(int id, SaveProviderDto dto)
        {
            var provider = await _db.Providers.FindAsync(id);
            if (provider is null) return false;

            provider.Name = dto.Name.Trim();
            provider.ContactEmail = dto.ContactEmail;
            provider.ContactPhone = dto.ContactPhone;
            provider.Website = dto.Website;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var provider = await _db.Providers.FindAsync(id);
            if (provider is null) return false;

            var hasBatches = await _db.OilBatches.AnyAsync(b => b.ProviderId == id);
            if (hasBatches)
                throw new InvalidOperationException(
                    "This provider still has batches recorded against it and cannot be deleted.");

            _db.Providers.Remove(provider);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
