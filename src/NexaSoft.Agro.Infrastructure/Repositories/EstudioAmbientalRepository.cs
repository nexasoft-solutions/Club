using Microsoft.EntityFrameworkCore;
using Dapper;
using NexaSoft.Agro.Application.Abstractions.Data;
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Constantes;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

using System.Text.Json;
using NexaSoft.Agro.Infrastructure.Serialization;
using static NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Dtos.EstudioAmbientalDto;
using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;
using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

namespace NexaSoft.Agro.Infrastructure.Repositories;

public class EstudioAmbientalRepository(ApplicationDbContext _dbContext, ISqlConnectionFactory _sqlConnectionFactory) : IEstudioAmbientalRepository
{
    public async Task<EstudioAmbientalDtoResponse?> GetEstudioAmbientalByIdAsync(long id, CancellationToken cancellationToken)
    {

        using var connection = _sqlConnectionFactory.CreateConnection();
     

        var sql = "SELECT fn_obtener_estudio_ambiental_completo(@id);";
        var estudioJson = await connection.QuerySingleOrDefaultAsync<string>(
        new CommandDefinition(sql, new { id }, cancellationToken: cancellationToken));

        //Console.WriteLine("JSON recibido:");
        //Console.WriteLine(estudioJson);

        return estudioJson is null
           ? null
           : JsonSerializer.Deserialize(
               estudioJson,
               AppJsonContext.Default.EstudioAmbientalDtoResponse); // Usando Source Generation

    }

    public async Task<EstudioAmbientalCapituloResponse> GetEstudioAmbientalCapitulosByIdAsync(long id, CancellationToken cancellationToken)
    {
        var estudio = await (
            from ea in _dbContext.Set<EstudioAmbiental>()
            where ea.Id == id && ea.FechaEliminacion == null
            select new EstudioAmbientalCapituloResponse(
                ea.Id,
                ea.Proyecto!,
                ea.FechaInicio,
                ea.FechaFin,
                ea.Detalles,
                ea.CodigoEstudio,
                ea.FechaCreacion,
                ea.EmpresaId,
                _dbContext.Set<Capitulo>()
                    .Where(c => c.EstudioAmbientalId == ea.Id && c.FechaEliminacion == null)
                    .OrderBy(c => c.NombreCapitulo) // o c.FechaCreacion
                    .Select(c => new CapitulosResponse(
                        c.Id,
                        c.NombreCapitulo!,
                        c.DescripcionCapitulo,
                        c.FechaCreacion
                    ))
                    .ToArray()
            )
        ).FirstOrDefaultAsync(cancellationToken);

        return estudio!;
    }

    public async Task<List<EstudioAmbientalEstructuraResponse>> GetEstudioAmbientalEstructurasByIdAsync(long id, CancellationToken cancellationToken)
    {
        var estructuras = await _dbContext.Set<Estructura>()
            .Where(e => e.SubCapituloId == id)
            .Select(e => new EstudioAmbientalEstructuraResponse
            (
                e.Id,
                e.TipoEstructuraId,
                e.NombreEstructura,
                e.DescripcionEstructura,
                e.PadreEstructuraId,
                e.SubCapituloId,
                e.EstadoEstructura,
                new List<EstudioAmbientalEstructuraResponse?>()
            ))
            .ToListAsync(cancellationToken);

        return ConstruirArbolEstructuras(estructuras);
    }

    public async Task<(Pagination<EstudioAmbientalResponse> Items, int TotalItems)> GetEstudiosAmbientalesAsync(ISpecification<EstudioAmbiental> spec, CancellationToken cancellationToken)
    {
        var queryBase = SpecificationEvaluator<EstudioAmbiental>.GetQuery(
           _dbContext.Set<EstudioAmbiental>().AsQueryable(), spec);

        var specParams = (spec as EstudioAmbientalSpecification)?.SpecParams ?? new BaseSpecParams();

        var totalItems = await _dbContext.Set<EstudioAmbiental>()
           .Where((spec as EstudioAmbientalSpecification)?.Criteria ?? (_ => true))
           .CountAsync(cancellationToken);

        var query = from c in queryBase

                    join EstadoEstudioAmbiental in _dbContext.Set<Constante>()
                        on new { Tipo = "EstadoEstudioAmbiental", Clave = c.EstadoEstudioAmbiental }
                        equals new { Tipo = EstadoEstudioAmbiental.TipoConstante, Clave = EstadoEstudioAmbiental.Clave }
                        into EstadoEstudioAmbientalJoin
                    from EstadoEstudioAmbientalConst in EstadoEstudioAmbientalJoin.DefaultIfEmpty()

                    select new EstudioAmbientalResponse(
                        c.Id,
                        c.Proyecto,
                        c.FechaInicio,
                        c.FechaFin,
                        c.Detalles,
                        EstadoEstudioAmbientalConst.Valor,
                        c.Empresa!.RazonSocial!,
                        c.EmpresaId,
                        c.CodigoEstudio,
                        c.FechaCreacion,
                        c.FechaModificacion,
                        c.UsuarioCreacion,
                        c.UsuarioModificacion
                    );

        var items = await query.ToListAsync(cancellationToken);

        var pagination = new Pagination<EstudioAmbientalResponse>(
              specParams.PageIndex,
              specParams.PageSize,
              totalItems,
              items
        );

        return (pagination, totalItems);
    }

    private List<EstudioAmbientalEstructuraResponse> ConstruirArbolEstructuras(List<EstudioAmbientalEstructuraResponse> estructuras)
    {
        var lookup = estructuras.ToDictionary(e => e.Id);
        var raiz = new List<EstudioAmbientalEstructuraResponse>();

        foreach (var estructura in estructuras)
        {
            if (estructura.PadreEstructuraId.HasValue && lookup.ContainsKey(estructura.PadreEstructuraId.Value))
            {
                var padre = lookup[estructura.PadreEstructuraId.Value];
                padre.Hijos.Add(estructura);
            }
            else
            {
                raiz.Add(estructura); // Es raíz (sin padre)
            }
        }

        return raiz;
    }
}
