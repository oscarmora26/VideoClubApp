using VideoClub.Shared.DTOs.Idiomas;

namespace VideoClub.Client.Services.Interfaces;

public interface IIdiomaService
{
    Task<List<IdiomaDto>> GetAllAsync();
}
