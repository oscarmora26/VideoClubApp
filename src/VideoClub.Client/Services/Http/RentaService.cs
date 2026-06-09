using System.Net.Http.Json;
using VideoClub.Client.Services.Interfaces;
using VideoClub.Shared.DTOs.Rentas;

namespace VideoClub.Client.Services.Http;

public class RentaService : IRentaService
{
    private readonly HttpClient _http;

    public RentaService(HttpClient http) => _http = http;

    public async Task<List<RentaDto>> GetAllAsync(string? search = null, string? estado = null, DateTime? desde = null, DateTime? hasta = null)
    {
        var query = new List<string>();
        if (!string.IsNullOrEmpty(search)) query.Add($"search={Uri.EscapeDataString(search)}");
        if (!string.IsNullOrEmpty(estado)) query.Add($"estado={Uri.EscapeDataString(estado)}");
        if (desde.HasValue) query.Add($"desde={desde.Value:yyyy-MM-dd}");
        if (hasta.HasValue) query.Add($"hasta={hasta.Value:yyyy-MM-dd}");
        var queryStr = query.Count > 0 ? "?" + string.Join("&", query) : "";
        return await _http.GetFromJsonAsync<List<RentaDto>>($"api/rentas{queryStr}") ?? [];
    }

    public async Task<RentaWithDetailsDto?> GetByIdAsync(long id) =>
        await _http.GetFromJsonAsync<RentaWithDetailsDto>($"api/rentas/{id}");

    public async Task<RentaWithDetailsDto?> CreateAsync(CreateRentaRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/rentas", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RentaWithDetailsDto>();
    }

    public async Task<RentaWithDetailsDto?> UpdateAsync(long id, UpdateRentaRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/rentas/{id}", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RentaWithDetailsDto>();
    }
}
