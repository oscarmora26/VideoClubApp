using System.Net.Http.Json;
using VideoClub.Client.Services.Interfaces;
using VideoClub.Shared.DTOs.Articulos;
using VideoClub.Shared.DTOs.ElencoArticulo;

namespace VideoClub.Client.Services.Http;

public class ArticuloService : IArticuloService
{
    private readonly HttpClient _http;

    public ArticuloService(HttpClient http) => _http = http;

    public async Task<List<ArticuloDto>> GetAllAsync(string? search = null, long? tipoArticuloId = null, bool? estado = null)
    {
        var query = new List<string>();
        if (!string.IsNullOrEmpty(search)) query.Add($"search={Uri.EscapeDataString(search)}");
        if (tipoArticuloId.HasValue) query.Add($"tipoArticuloId={tipoArticuloId.Value}");
        if (estado.HasValue) query.Add($"estado={estado.Value}");
        var queryStr = query.Count > 0 ? "?" + string.Join("&", query) : "";
        return await _http.GetFromJsonAsync<List<ArticuloDto>>($"api/articulos{queryStr}") ?? [];
    }

    public async Task<ArticuloDto?> GetByIdAsync(long id) =>
        await _http.GetFromJsonAsync<ArticuloDto>($"api/articulos/{id}");

    public async Task<ArticuloDto?> CreateAsync(CreateArticuloRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/articulos", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ArticuloDto>();
    }

    public async Task<ArticuloDto?> UpdateAsync(long id, UpdateArticuloRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/articulos/{id}", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ArticuloDto>();
    }

    public async Task<bool> ToggleEstadoAsync(long id)
    {
        var response = await _http.PatchAsync($"api/articulos/{id}/toggle-estado", null);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, bool>>();
        return result?.GetValueOrDefault("estado") ?? false;
    }

    public async Task<List<ElencoArticuloDto>> GetElencoAsync(long articuloId) =>
        await _http.GetFromJsonAsync<List<ElencoArticuloDto>>($"api/articulos/{articuloId}/elenco") ?? [];

    public async Task<ElencoArticuloDto?> AddElencoAsync(long articuloId, AddElencoToArticuloRequest request)
    {
        var response = await _http.PostAsJsonAsync($"api/articulos/{articuloId}/elenco", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ElencoArticuloDto>();
    }

    public async Task<ElencoArticuloDto?> UpdateElencoAsync(long articuloId, long elencoId, UpdateArticuloElencoRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/articulos/{articuloId}/elenco/{elencoId}", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ElencoArticuloDto>();
    }

    public async Task<bool> RemoveElencoAsync(long articuloId, long elencoId, long rolElencoId)
    {
        var response = await _http.DeleteAsync($"api/articulos/{articuloId}/elenco/{elencoId}/{rolElencoId}");
        return response.IsSuccessStatusCode;
    }
}
