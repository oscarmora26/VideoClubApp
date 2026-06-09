using System.Net.Http.Json;
using VideoClub.Client.Services.Interfaces;
using VideoClub.Shared.DTOs.TiposArticulos;
using VideoClub.Shared.DTOs.Generos;

namespace VideoClub.Client.Services.Http;

public class TipoArticuloService : ITipoArticuloService
{
    private readonly HttpClient _http;

    public TipoArticuloService(HttpClient http) => _http = http;

    public async Task<List<TipoArticuloDto>> GetAllAsync() =>
        await _http.GetFromJsonAsync<List<TipoArticuloDto>>("api/tipos-articulos") ?? [];

    public async Task<List<GeneroDto>> GetGenerosByTipoAsync(long tipoArticuloId) =>
        await _http.GetFromJsonAsync<List<GeneroDto>>($"api/tipos-articulos/{tipoArticuloId}/generos") ?? [];
}
