using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Client.Services.Interfaces;

public interface IRolElencoService
{
    Task<List<RolElencoDto>> GetAllAsync();
    Task<RolElencoDto?> CreateAsync(CreateRolElencoRequest request);
    Task<RolElencoDto?> UpdateAsync(long id, UpdateRolElencoRequest request);
    Task<bool> DeleteAsync(long id);
}
