using VideoClub.Shared.DTOs.Articulos;
using VideoClub.Shared.DTOs.ElencoArticulo;

namespace VideoClub.Client.Services.Interfaces;

public interface IArticuloService
{
    Task<List<ArticuloDto>> GetAllAsync(string? search = null, long? tipoArticuloId = null, bool? estado = null);
    Task<ArticuloDto?> GetByIdAsync(long id);
    Task<ArticuloDto?> CreateAsync(CreateArticuloRequest request);
    Task<ArticuloDto?> UpdateAsync(long id, UpdateArticuloRequest request);
    Task<bool> ToggleEstadoAsync(long id);
    Task<List<ElencoArticuloDto>> GetElencoAsync(long articuloId);
    Task<ElencoArticuloDto?> AddElencoAsync(long articuloId, AddElencoToArticuloRequest request);
    Task<ElencoArticuloDto?> UpdateElencoAsync(long articuloId, long elencoId, UpdateArticuloElencoRequest request);
    Task<bool> RemoveElencoAsync(long articuloId, long elencoId);
}
