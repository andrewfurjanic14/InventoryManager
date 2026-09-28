using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using InventoryManager.Shared.Dtos;
using InventoryManager.Shared;

namespace InventoryManager.Client.Services
{
    public class OilBatchService
    {
        private readonly HttpClient _http;

        public OilBatchService(HttpClient http)
        {
            _http = http;
        }

        public Task<List<OilBatchDto>?> GetAllAsync(int? oilId = null, BatchStatus? status = null)
        {
            var query = "api/oilbatches";
            var hasQuery = false;
            if (oilId.HasValue) { query += (hasQuery ? "&" : "?") + $"oilId={oilId.Value}"; hasQuery = true; }
            if (status.HasValue) { query += (hasQuery ? "&" : "?") + $"status={status.Value}"; }
            return _http.GetFromJsonAsync<List<OilBatchDto>>(query);
        }

        public Task<OilBatchDto?> GetByIdAsync(int id) => _http.GetFromJsonAsync<OilBatchDto>($"api/oilbatches/{id}");

        public async Task<OilBatchDto?> CreateAsync(CreateOilBatchDto dto)
        {
            var res = await _http.PostAsJsonAsync("api/oilbatches", dto);
            return await res.Content.ReadFromJsonAsync<OilBatchDto>();
        }

        public async Task<bool> UpdateAsync(int id, UpdateOilBatchDto dto)
        {
            var res = await _http.PutAsJsonAsync($"api/oilbatches/{id}", dto);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await _http.DeleteAsync($"api/oilbatches/{id}");
            return res.IsSuccessStatusCode;
        }
    }
}
