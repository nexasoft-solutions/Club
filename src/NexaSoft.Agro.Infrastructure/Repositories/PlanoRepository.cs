using Microsoft.EntityFrameworkCore;
using NexaSoft.Agro.Application.Features.Proyectos.Planos;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Constantes;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos;

namespace NexaSoft.Agro.Infrastructure.Repositories;

public class PlanoRepository(ApplicationDbContext _dbContext) : IPlanoRepository
{
    public async Task<Result<Plano>> GetPlanoByIdDetalle(Guid PlanoId, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<Plano>()
           .Include(p => p.Detalles)
           .FirstOrDefaultAsync(p => p.Id == PlanoId, cancellationToken);
        if (entity is null)
            return Result.Failure<Plano>(PlanoErrores.NoEncontrado);

        return entity;
    }


    public async Task<Result<PlanoItemResponse>> GetPlanoArchivoById(Guid ArchivoId, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<Plano>()
        .Include(p => p.Detalles)
        .Include(p => p.Colaborador)   // Incluir Colaborador para el nombre de usuario
        .FirstOrDefaultAsync(p => p.ArchivoId == ArchivoId, cancellationToken);

        if (entity is null)
            return Result.Failure<PlanoItemResponse>(PlanoErrores.NoEncontrado);

        // Obtener la escala traducida desde la tabla de constantes
        var escalaValor = await _dbContext.Set<Constante>()
            .Where(c => c.TipoConstante == "Escala" && c.Clave == entity.EscalaId)
            .Select(c => c.Valor)
            .FirstOrDefaultAsync(cancellationToken);

        // Mapear a PlanoResponse
        var response = new PlanoItemResponse(
            Id: entity.Id,
            Escala: escalaValor,
            SistemaProyeccion: entity.SistemaProyeccion,
            NombrePlano: entity.NombrePlano,
            CodigoPlano: entity.CodigoPlano,
            UserName: entity.Colaborador?.UserName ?? string.Empty,
            ArchivoId: entity.ArchivoId,
            EscalaId: entity.EscalaId,
            Detalles: entity.Detalles.Select(d => new PlanoDetalleResponse(
                Id: d.Id,
                Descripcion: d.Descripcion,
                Coordenadas: d.Coordenadas
            )).ToList(),
            FechaCreacion: entity.FechaCreacion
        );

        return Result.Success(response);

    }

    public async Task<(Pagination<PlanoResponse> Items, int TotalItems)> GetPlanosAsync(ISpecification<Plano> spec, CancellationToken cancellationToken)
    {
        var specParams = (spec as PlanoSpecification)?.SpecParams ?? new BaseSpecParams();

        var query = SpecificationEvaluator<Plano>.GetQuery(
            _dbContext.Set<Plano>()
                .Include(p => p.Detalles)
                .Include(p => p.Archivo)
                .Include(p => p.Colaborador),
            spec
        );

        var totalItems = await query.CountAsync(cancellationToken);

        var planos = await query
            .Skip(specParams.PageSize * (specParams.PageIndex - 1))
            .Take(specParams.PageSize)
            .ToListAsync(cancellationToken);

        var items = planos.Select(p => new PlanoResponse(
            Id: p.Id,
            Escala: _dbContext.Set<Constante>()
                      .Where(c => c.TipoConstante == "Escala" && c.Clave == p.EscalaId)
                      .Select(c => c.Valor)
                      .FirstOrDefault(),
            SistemaProyeccion: p.SistemaProyeccion,
            NombrePlano: p.NombrePlano,
            CodigoPlano: p.CodigoPlano,
            NombreArchivo: p.Archivo?.NombreArchivo ?? string.Empty,
            UserName: p.Colaborador?.UserName ?? string.Empty,
            ArchivoId: p.ArchivoId,
            FechaCreacion: p.FechaCreacion,
            Detalles: p.Detalles.Select(d => new PlanoDetalleResponse(
                d.Id,
                d.Descripcion,
                d.Coordenadas
            )).ToList()
        )).ToList();

        return (
            new Pagination<PlanoResponse>(
                specParams.PageIndex,
                specParams.PageSize,
                totalItems,
                items
            ),
            totalItems
        );

        /*var queryBase = SpecificationEvaluator<Plano>.GetQuery(
             _dbContext.Set<Plano>().AsQueryable(), spec);

          var specParams = (spec as PlanoSpecification)?.SpecParams ?? new BaseSpecParams();

          var totalItems = await _dbContext.Set<Plano>()
             .Where((spec as PlanoSpecification)?.Criteria ?? (_ => true))
             .CountAsync(cancellationToken);

          var query = from c in queryBase

                      join Escala in _dbContext.Set<Constante>()
                          on new { Tipo = "Escala", Clave = c.Escala }
                          equals new { Tipo = Escala.TipoConstante, Clave = Escala.Clave }
                          into EscalaJoin
                      from EscalaConst in EscalaJoin.DefaultIfEmpty()

                      select new PlanoResponse(
                          c.Id,
                          EscalaConst.Valor,
                          c.SistemaProyeccion,
                          c.NombrePlano,
                          c.CodigoPlano,
                          c.Archivo!.NombreArchivo!,
                          c.Colaborador!.UserName!,
                          c.FechaCreacion
                      );

          var items = await query.ToListAsync(cancellationToken);

          var pagination = new Pagination<PlanoResponse>(
                specParams.PageIndex,
                specParams.PageSize,
                totalItems,
                items
          );

          return (pagination, totalItems);*/

    }
}
