using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Client.Services.Interfaces;

public interface IRolElencoService
{
    Task<List<RolElencoDto>> GetAllAsync();
}
