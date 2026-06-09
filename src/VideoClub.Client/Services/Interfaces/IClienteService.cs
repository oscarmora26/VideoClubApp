using VideoClub.Shared.DTOs.Clientes;

namespace VideoClub.Client.Services.Interfaces;

public interface IClienteService
{
    Task<List<ClienteDto>> GetAllAsync(string? search = null, bool? estado = null);
    Task<ClienteDto?> GetByIdAsync(long id);
    Task<ClienteDto?> CreateAsync(CreateClienteRequest request);
    Task<ClienteDto?> UpdateAsync(long id, UpdateClienteRequest request);
    Task<bool> ToggleEstadoAsync(long id);
}
