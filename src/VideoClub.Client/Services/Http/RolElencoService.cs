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
}
