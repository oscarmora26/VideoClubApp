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
}
