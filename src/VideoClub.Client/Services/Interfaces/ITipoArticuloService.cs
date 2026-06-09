using VideoClub.Shared.DTOs.TiposArticulos;
using VideoClub.Shared.DTOs.Generos;

namespace VideoClub.Client.Services.Interfaces;

public interface ITipoArticuloService
{
    Task<List<TipoArticuloDto>> GetAllAsync();
    Task<List<GeneroDto>> GetGenerosByTipoAsync(long tipoArticuloId);
}
