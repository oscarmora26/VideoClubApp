using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data;
using VideoClub.Api.Features.Elenco.Queries;
using VideoClub.Shared;
using VideoClub.Shared.DTOs.Elenco;
using VideoClub.Shared.DTOs.RolesElenco;

namespace VideoClub.Api.Features.Elenco.Handlers;

public class GetAllElencoConRolesHandler : IRequestHandler<GetAllElencoConRolesQuery, Result<List<ElencoConRolesDto>>>
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GetAllElencoConRolesHandler(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<ElencoConRolesDto>>> Handle(GetAllElencoConRolesQuery request, CancellationToken ct)
    {
        var query = _db.Elenco.AsQueryable();

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(e => EF.Functions.ILike(e.Nombre, $"%{request.Search}%"));

        var personas = await query.ToListAsync(ct);

        var dtos = new List<ElencoConRolesDto>();
        foreach (var persona in personas)
        {
            var rolesFromArticulos = await _db.ElencosArticulos
                .Where(ea => ea.ElencoId == persona.Id)
                .Select(ea => ea.RolElenco)
                .Distinct()
                .ToListAsync(ct);

            var rolesFromDirect = await _db.ElencosRoles
                .Where(er => er.ElencoId == persona.Id)
                .Select(er => er.RolElenco)
                .ToListAsync(ct);

            var roles = rolesFromArticulos.Union(rolesFromDirect).Distinct().ToList();

            dtos.Add(new ElencoConRolesDto
            {
                Id = persona.Id,
                Nombre = persona.Nombre,
                Roles = _mapper.Map<List<RolElencoDto>>(roles)
            });
        }

        return Result<List<ElencoConRolesDto>>.Success(dtos);
    }
}
