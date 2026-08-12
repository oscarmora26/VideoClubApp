using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Rentas.Queries;

namespace VideoClub.Api.Features.Rentas.Handlers;

public class ExportRentasHandler : IRequestHandler<ExportRentasQuery, byte[]>
{
    private readonly AppDbContext _db;

    public ExportRentasHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<byte[]> Handle(ExportRentasQuery request, CancellationToken ct)
    {
        var query = _db.Rentas
            .Include(r => r.Cliente)
            .Include(r => r.Detalles)
                .ThenInclude(d => d.Articulo)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(r => r.NoRenta.Contains(request.Search) ||
                r.Cliente.Nombre.ToLower().Contains(request.Search.ToLower()) ||
                r.Cliente.Cedula.ToLower().Contains(request.Search.ToLower()));

        if (!string.IsNullOrEmpty(request.Estado))
            query = query.Where(r => r.EstadoRenta == request.Estado);

        if (request.Desde.HasValue)
        {
            var desde = DateTime.SpecifyKind(request.Desde.Value, DateTimeKind.Utc);
            query = query.Where(r => r.FechaRenta >= desde);
        }

        if (request.Hasta.HasValue)
        {
            var hasta = DateTime.SpecifyKind(request.Hasta.Value.AddDays(1), DateTimeKind.Utc);
            query = query.Where(r => r.FechaRenta < hasta);
        }

        var rentas = await query
            .OrderByDescending(r => r.FechaRenta)
            .ToListAsync(ct);

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Rentas");

        ws.Cell(1, 1).Value = "No. Renta";
        ws.Cell(1, 2).Value = "Cliente";
        ws.Cell(1, 3).Value = "Fecha Renta";
        ws.Cell(1, 4).Value = "Estado";
        ws.Cell(1, 5).Value = "Total";
        ws.Cell(1, 6).Value = "Artículos";

        var headerRange = ws.Range(1, 1, 1, 6);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.FromArgb(0x402BCA);
        headerRange.Style.Font.FontColor = XLColor.White;

        for (int i = 0; i < rentas.Count; i++)
        {
            var row = i + 2;
            ws.Cell(row, 1).Value = rentas[i].NoRenta;
            ws.Cell(row, 2).Value = rentas[i].Cliente.Nombre;
            ws.Cell(row, 3).Value = rentas[i].FechaRenta.ToString("dd/MM/yyyy");
            ws.Cell(row, 4).Value = rentas[i].EstadoRenta;
            ws.Cell(row, 5).Value = rentas[i].MontoTotal;
            ws.Cell(row, 5).Style.NumberFormat.Format = "#,##0.00";
            ws.Cell(row, 6).Value = string.Join(", ", rentas[i].Detalles.Select(d => d.Articulo.Titulo));
        }

        ws.Columns().AdjustToContents();

        using var ms = new MemoryStream();
        workbook.SaveAs(ms);
        return ms.ToArray();
    }
}
