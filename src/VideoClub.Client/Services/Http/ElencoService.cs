using System.Net.Http.Json;
using VideoClub.Client.Services.Interfaces;
using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Client.Services.Http;

public class ElencoService : IElencoService
{
    private readonly HttpClient _http;

    public ElencoService(HttpClient http) => _http = http;

    public async Task<List<ElencoDto>> GetAllAsync(string? search = null)
    {
        var query = !string.IsNullOrEmpty(search) ? $"?search={Uri.EscapeDataString(search)}" : "";
        return await _http.GetFromJsonAsync<List<ElencoDto>>($"api/elenco{query}") ?? [];
    }

    public async Task<List<ElencoConRolesDto>> GetAllConRolesAsync(string? search = null)
    {
        var query = !string.IsNullOrEmpty(search) ? $"?search={Uri.EscapeDataString(search)}" : "";
        return await _http.GetFromJsonAsync<List<ElencoConRolesDto>>($"api/elenco/con-roles{query}") ?? [];
    }

    public async Task<ElencoDto?> CreateAsync(CreateElencoRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/elenco", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ElencoDto>();
    }

    public async Task<ElencoDto?> UpdateAsync(long id, UpdateElencoRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/elenco/{id}", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ElencoDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var response = await _http.DeleteAsync($"api/elenco/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetRolesAsync(long id, List<long> rolElencoIds)
    {
        var response = await _http.PutAsJsonAsync($"api/elenco/{id}/roles", new { rolElencoIds });
        return response.IsSuccessStatusCode;
    }
}
