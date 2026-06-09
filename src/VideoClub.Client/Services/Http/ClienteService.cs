using System.Net.Http.Json;
using VideoClub.Client.Services.Interfaces;
using VideoClub.Shared.DTOs.Clientes;

namespace VideoClub.Client.Services.Http;

public class ClienteService : IClienteService
{
    private readonly HttpClient _http;

    public ClienteService(HttpClient http) => _http = http;

    public async Task<List<ClienteDto>> GetAllAsync(string? search = null, bool? estado = null)
    {
        var query = new List<string>();
        if (!string.IsNullOrEmpty(search)) query.Add($"search={Uri.EscapeDataString(search)}");
        if (estado.HasValue) query.Add($"estado={estado.Value}");
        var queryStr = query.Count > 0 ? "?" + string.Join("&", query) : "";
        return await _http.GetFromJsonAsync<List<ClienteDto>>($"api/clientes{queryStr}") ?? [];
    }

    public async Task<ClienteDto?> GetByIdAsync(long id) =>
        await _http.GetFromJsonAsync<ClienteDto>($"api/clientes/{id}");

    public async Task<ClienteDto?> CreateAsync(CreateClienteRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/clientes", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClienteDto>();
    }

    public async Task<ClienteDto?> UpdateAsync(long id, UpdateClienteRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/clientes/{id}", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClienteDto>();
    }

    public async Task<bool> ToggleEstadoAsync(long id)
    {
        var response = await _http.PatchAsync($"api/clientes/{id}/toggle-estado", null);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, bool>>();
        return result?.GetValueOrDefault("estado") ?? false;
    }
}
