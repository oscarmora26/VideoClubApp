using VideoClub.Shared.DTOs.Rentas;

namespace VideoClub.Client.Services.Interfaces;

public interface IRentaService
{
    Task<List<RentaDto>> GetAllAsync(string? search = null, string? estado = null, DateTime? desde = null, DateTime? hasta = null);
    Task<RentaWithDetailsDto?> GetByIdAsync(long id);
    Task<RentaWithDetailsDto?> CreateAsync(CreateRentaRequest request);
    Task<RentaWithDetailsDto?> UpdateAsync(long id, UpdateRentaRequest request);
    Task<byte[]> ExportExcelAsync(string? search, string? estado, DateTime? desde, DateTime? hasta);
}
