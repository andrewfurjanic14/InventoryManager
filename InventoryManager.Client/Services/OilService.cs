using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using InventoryManager.Shared.Dtos;

namespace InventoryManager.Client.Services
{
    public class OilService
    {
        private readonly HttpClient _http;

        public OilService(HttpClient http)
        {
            _http = http;
        }

        public Task<List<OilDto>?> GetAllAsync() => _http.GetFromJsonAsync<List<OilDto>>("api/oils");
        public Task<OilDto?> GetByIdAsync(int id) => _http.GetFromJsonAsync<OilDto>($"api/oils/{id}");
        public async Task<OilDto?> CreateAsync(SaveOilDto dto)
        {
            var res = await _http.PostAsJsonAsync("api/oils", dto);
            return await res.Content.ReadFromJsonAsync<OilDto>();
        }

        public async Task<bool> UpdateAsync(int id, SaveOilDto dto)
        {
            var res = await _http.PutAsJsonAsync($"api/oils/{id}", dto);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await _http.DeleteAsync($"api/oils/{id}");
            return res.IsSuccessStatusCode;
        }
    }
}
