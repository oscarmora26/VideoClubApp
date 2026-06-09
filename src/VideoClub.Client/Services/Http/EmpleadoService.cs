using System.Net.Http.Json;
using VideoClub.Client.Services.Interfaces;
using VideoClub.Shared.DTOs.Empleados;

namespace VideoClub.Client.Services.Http;

public class EmpleadoService : IEmpleadoService
{
    private readonly HttpClient _http;

    public EmpleadoService(HttpClient http) => _http = http;

    public async Task<List<EmpleadoDto>> GetAllAsync() =>
        await _http.GetFromJsonAsync<List<EmpleadoDto>>("api/empleados") ?? [];
}
