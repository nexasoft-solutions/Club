using Microsoft.EntityFrameworkCore;
using NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Constantes;
using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;

namespace NexaSoft.Agro.Infrastructure.Repositories;

public class ColaboradorRepository(ApplicationDbContext _dbContext) : IColaboradorRepository
{
    public async Task<(Pagination<ColaboradorResponse> Items, int TotalItems)> GetColaboradoresAsync(ISpecification<Colaborador> spec, CancellationToken cancellationToken)
    {
        var queryBase = SpecificationEvaluator<Colaborador>.GetQuery(
           _dbContext.Set<Colaborador>().AsQueryable(), spec);

        var specParams = (spec as ColaboradorSpecification)?.SpecParams ?? new BaseSpecParams();

        var totalItems = await _dbContext.Set<Colaborador>()
           .Where((spec as ColaboradorSpecification)?.Criteria ?? (_ => true))
           .CountAsync(cancellationToken);

        var query = from c in queryBase

                    join TipoDocumento in _dbContext.Set<Constante>()
                        on new { Tipo = "TipoDocumento", Clave = c.TipoDocumentoId }
                        equals new { Tipo = TipoDocumento.TipoConstante, Clave = TipoDocumento.Clave }
                        into TipoDocumentoJoin
                    from TipoDocumentoConst in TipoDocumentoJoin.DefaultIfEmpty()

                    join Genero in _dbContext.Set<Constante>()
                        on new { Tipo = "Genero", Clave = c.GeneroColaboradorId }
                        equals new { Tipo = Genero.TipoConstante, Clave = Genero.Clave }
                        into GeneroJoin
                    from GeneroConst in GeneroJoin.DefaultIfEmpty()

                    join EstadoCivil in _dbContext.Set<Constante>()
                        on new { Tipo = "EstadoCivil", Clave = c.EstadoCivilColaboradorId }
                        equals new { Tipo = EstadoCivil.TipoConstante, Clave = EstadoCivil.Clave }
                        into EstadoCivilJoin
                    from EstadoCivilConst in EstadoCivilJoin.DefaultIfEmpty()

                    join Cargo in _dbContext.Set<Constante>()
                        on new { Tipo = "Cargo", Clave = c.CargoId }
                        equals new { Tipo = Cargo.TipoConstante, Clave = Cargo.Clave }
                        into CargoJoin
                    from CargoConst in CargoJoin.DefaultIfEmpty()

                    join Departamento in _dbContext.Set<Constante>()
                        on new { Tipo = "Departamento", Clave = c.DepartamentoId }
                        equals new { Tipo = Departamento.TipoConstante, Clave = Departamento.Clave }
                        into DepartamentoJoin
                    from DepartamentoConst in DepartamentoJoin.DefaultIfEmpty()

                    select new ColaboradorResponse(
                        c.Id,
                        c.NombresColaborador,
                        c.ApellidosColaborador,
                        c.NombreCompletoColaborador,
                        TipoDocumentoConst.Valor,
                        c.NumeroDocumentoIdentidad,
                        c.FechaNacimiento,
                        GeneroConst.Valor,
                        EstadoCivilConst.Valor,
                        c.Direccion,
                        c.CorreoElectronico,
                        c.TelefonoMovil,
                        CargoConst.Valor,
                        DepartamentoConst.Valor,
                        c.FechaIngreso,
                        c.Salario,
                        c.FechaCese,
                        c.Comentarios,
                        c.ConsultoraId,
                        c.Consultora!.NombreConsultora,
                        //c.Consultora!.RucConsultora,
                        c.UserName,
                        c.TipoDocumentoId,
                        c.GeneroColaboradorId,
                        c.EstadoCivilColaboradorId,
                        c.CargoId,
                        c.DepartamentoId,
                        c.FechaCreacion
                    );

        var items = await query.ToListAsync(cancellationToken);

        var pagination = new Pagination<ColaboradorResponse>(
              specParams.PageIndex,
              specParams.PageSize,
              totalItems,
              items
        );

        return (pagination, totalItems);

    }
}
