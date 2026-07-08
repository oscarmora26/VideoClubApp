using System.Net.Http.Json;
using VideoClub.Client.Services.Interfaces;
using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Client.Services.Http;

public class RolElencoService : IRolElencoService
{
    private readonly HttpClient _http;

    public RolElencoService(HttpClient http) => _http = http;

    public async Task<List<RolElencoDto>> GetAllAsync() =>
        await _http.GetFromJsonAsync<List<RolElencoDto>>("api/roles-elenco") ?? [];

    public async Task<RolElencoDto?> CreateAsync(CreateRolElencoRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/roles-elenco", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RolElencoDto>();
    }

    public async Task<RolElencoDto?> UpdateAsync(long id, UpdateRolElencoRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/roles-elenco/{id}", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RolElencoDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var response = await _http.DeleteAsync($"api/roles-elenco/{id}");
        return response.IsSuccessStatusCode;
    }
}
