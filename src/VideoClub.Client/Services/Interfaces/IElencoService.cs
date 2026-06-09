using VideoClub.Shared.DTOs.Elenco;

namespace VideoClub.Client.Services.Interfaces;

public interface IElencoService
{
    Task<List<ElencoDto>> GetAllAsync(string? search = null);
}
