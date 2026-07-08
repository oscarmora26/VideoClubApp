using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Client.Services.Interfaces;

public interface IElencoService
{
    Task<List<ElencoDto>> GetAllAsync(string? search = null);
    Task<List<ElencoConRolesDto>> GetAllConRolesAsync(string? search = null);
    Task<ElencoDto?> CreateAsync(CreateElencoRequest request);
    Task<ElencoDto?> UpdateAsync(long id, UpdateElencoRequest request);
    Task<bool> DeleteAsync(long id);
    Task<bool> SetRolesAsync(long id, List<long> rolElencoIds);
}
