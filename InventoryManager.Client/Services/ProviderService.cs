using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using InventoryManager.Shared.Dtos;

namespace InventoryManager.Client.Services
{
    public class ProviderService
    {
        private readonly HttpClient _http;

        public ProviderService(HttpClient http)
        {
            _http = http;
        }

        public Task<List<ProviderDto>?> GetAllAsync() => _http.GetFromJsonAsync<List<ProviderDto>>("api/providers");
        public Task<ProviderDto?> GetByIdAsync(int id) => _http.GetFromJsonAsync<ProviderDto>($"api/providers/{id}");
        public async Task<ProviderDto?> CreateAsync(SaveProviderDto dto)
        {
            var res = await _http.PostAsJsonAsync("api/providers", dto);
            return await res.Content.ReadFromJsonAsync<ProviderDto>();
        }

        public async Task<bool> UpdateAsync(int id, SaveProviderDto dto)
        {
            var res = await _http.PutAsJsonAsync($"api/providers/{id}", dto);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await _http.DeleteAsync($"api/providers/{id}");
            return res.IsSuccessStatusCode;
        }
    }
}
