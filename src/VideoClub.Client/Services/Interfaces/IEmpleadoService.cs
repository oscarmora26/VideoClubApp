using VideoClub.Shared.DTOs.Empleados;

namespace VideoClub.Client.Services.Interfaces;

public interface IEmpleadoService
{
    Task<List<EmpleadoDto>> GetAllAsync();
}
