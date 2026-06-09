using System.Net.Http.Json;
using VideoClub.Client.Services.Interfaces;
using VideoClub.Shared.DTOs.Idiomas;

namespace VideoClub.Client.Services.Http;

public class IdiomaService : IIdiomaService
{
    private readonly HttpClient _http;

    public IdiomaService(HttpClient http) => _http = http;

    public async Task<List<IdiomaDto>> GetAllAsync() =>
        await _http.GetFromJsonAsync<List<IdiomaDto>>("api/idiomas") ?? [];
}
