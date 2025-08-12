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
    public async Task<EstudioAmbientalDtoResponse?> GetEstudioAmbientalByIdAsync(Guid id, CancellationToken cancellationToken)
    {

        using var connection = _sqlConnectionFactory.CreateConnection();
        // Definir la consulta para llamar al procedimiento almacenado
        /*var sql = """
                        WITH RECURSIVE estructuras_hijos AS (
                        SELECT DISTINCT ON (e.id)
                            e.id,
                            e.tipo_estructura,
                            e.descripcion_estructura,
                            e.padre_estructura_id,
                            e.sub_capitulo_id
                        FROM estructuras e
                        WHERE e.fecha_eliminacion IS NULL
                        
                        UNION ALL
                        
                        SELECT DISTINCT ON (e.id)
                            e.id,
                            e.tipo_estructura,
                            e.descripcion_estructura,
                            e.padre_estructura_id,
                            e.sub_capitulo_id
                        FROM estructuras e
                        JOIN estructuras_hijos eh ON e.padre_estructura_id = eh.id
                        WHERE e.fecha_eliminacion IS NULL
                    )
                    SELECT json_build_object(
                        'Id', e.id,
                        'Proyecto', e.proyecto,
                        'FechaInicio', e.fecha_inicio,
                        'FechaFin', e.fecha_fin,
                        'Detalles', e.detalles,
                        'FechaCreacionEstudio', e.fecha_creacion,
                        'Capitulos', (
                            SELECT json_agg(
                                json_build_object(
                                    'Id', c.id,
                                    'NombreCapitulo', c.nombre_capitulo,
                                    'DescripcionCapitulo', c.descripcion_capitulo,
                                    'FechaCreacionCapitulo', c.fecha_creacion,
                                    'SubCapitulos', (
                                        SELECT json_agg(
                                            json_build_object(
                                                'Id', sc.id,
                                                'NombreSubCapitulo', sc.nombre_sub_capitulo,
                                                'DescripcionSubCapitulo', sc.descripcion_sub_capitulo,
                                                'FechaCreacionSubCapitulo', sc.fecha_creacion,
                                                'Estructuras', (
                                                    SELECT COALESCE(
                                                        json_agg(
                                                            json_build_object(
                                                                'Id', eh.id,
                                                                'TipoEstructura', eh.tipo_estructura,
                                                                'DescripcionEstructura', eh.descripcion_estructura,
                                                                'PadreEstructuraId', eh.padre_estructura_id,
                                                                'SubCapituloId', eh.sub_capitulo_id,
                                                                'Hijos', (
                                                                    SELECT COALESCE(
                                                                        json_agg(
                                                                            json_build_object(
                                                                                'Id', eh2.id,
                                                                                'TipoEstructura', eh2.tipo_estructura,
                                                                                'DescripcionEstructura', eh2.descripcion_estructura,
                                                                                'PadreEstructuraId', eh2.padre_estructura_id,
                                                                                'SubCapituloId', eh2.sub_capitulo_id,
                                                                                'Archivos', (
                                                                                    SELECT COALESCE(
                                                                                        json_agg(
                                                                                            json_build_object(
                                                                                                'Id', a.id,
                                                                                                'Nombre', a.nombre_archivo,
                                                                                                'DescripcionArchivo', a.descripcion_archivo,
                                                                                                'RutaArchivo', a.ruta_archivo,
                                                                                                'FechaCarga', a.fecha_carga,
                                                                                                'TipoArchivo', a.tipo_archivo,
                                                                                                'FechaCreacionArchivo', a.fecha_creacion,
                                                                                                'Plano', CASE 
                                                                                                    WHEN p.id IS NOT NULL THEN json_build_object(
                                                                                                        'Id', p.id,
                                                                                                        'Escala', p.escala,
                                                                                                        'SistemaProyeccion', p.sistema_proyeccion,
                                                                                                        'NombrePlano', p.nombre_plano,
                                                                                                        'CodigoPlano', p.codigo_plano,
                                                                                                        'ColaboradorId', p.colaborador_id,
                                                                                                        'FechaCreacionPlano', p.fecha_creacion,
                                                                                                        'Detalles', (
                                                                                                            SELECT COALESCE(
                                                                                                                json_agg(
                                                                                                                    json_build_object(
                                                                                                                        'DescripcionPlano', pd.descripcion,
                                                                                                                        'Latitud', pd.latitud_plano,
                                                                                                                        'Longitud', pd.longitud_plano,
                                                                                                                        'Area', pd.area
                                                                                                                    )
                                                                                                                ),
                                                                                                                '[]'::json
                                                                                                            )
                                                                                                            FROM plano_detalle pd
                                                                                                            WHERE pd.plano_id = p.id
                                                                                                        )
                                                                                                    )
                                                                                                    ELSE NULL
                                                                                                END
                                                                                            )
                                                                                        ),
                                                                                        '[]'::json
                                                                                    )
                                                                                    FROM archivos a
                                                                                    LEFT JOIN planos p ON p.archivo_id = a.id AND p.fecha_eliminacion IS NULL
                                                                                    WHERE a.estructura_id = eh2.id
                                                                                    AND a.sub_capitulo_id IS NULL  -- Solo archivos de estructura
                                                                                    AND a.fecha_eliminacion IS NULL
                                                                                )
                                                                            )
                                                                        ),
                                                                        '[]'::json
                                                                    )
                                                                    FROM estructuras_hijos eh2
                                                                    WHERE eh2.padre_estructura_id = eh.id
                                                                ),
                                                                'Archivos', (
                                                                    SELECT COALESCE(
                                                                        json_agg(
                                                                            json_build_object(
                                                                                'Id', a.id,
                                                                                'Nombre', a.nombre_archivo,
                                                                                'DescripcionArchivo', a.descripcion_archivo,
                                                                                'RutaArchivo', a.ruta_archivo,
                                                                                'FechaCarga', a.fecha_carga,
                                                                                'TipoArchivo', a.tipo_archivo,
                                                                                'FechaCreacionArchivo', a.fecha_creacion,
                                                                                'Plano', CASE 
                                                                                    WHEN p.id IS NOT NULL THEN json_build_object(
                                                                                        'Id', p.id,
                                                                                        'Escala', p.escala,
                                                                                        'SistemaProyeccion', p.sistema_proyeccion,
                                                                                        'NombrePlano', p.nombre_plano,
                                                                                        'CodigoPlano', p.codigo_plano,
                                                                                        'ColaboradorId', p.colaborador_id,
                                                                                        'FechaCreacionPlano', p.fecha_creacion,
                                                                                        'Detalles', (
                                                                                            SELECT COALESCE(
                                                                                                json_agg(
                                                                                                    json_build_object(
                                                                                                        'DescripcionPlano', pd.descripcion,
                                                                                                        'Latitud', pd.latitud_plano,
                                                                                                        'Longitud', pd.longitud_plano,
                                                                                                        'Area', pd.area
                                                                                                    )
                                                                                                ),
                                                                                                '[]'::json
                                                                                            )
                                                                                            FROM plano_detalle pd
                                                                                            WHERE pd.plano_id = p.id
                                                                                        )
                                                                                    )
                                                                                    ELSE NULL
                                                                                END
                                                                            )
                                                                        ),
                                                                        '[]'::json
                                                                    )
                                                                    FROM archivos a
                                                                    LEFT JOIN planos p ON p.archivo_id = a.id AND p.fecha_eliminacion IS NULL
                                                                    WHERE a.estructura_id = eh.id
                                                                    AND a.sub_capitulo_id IS NULL  -- Solo archivos de estructura
                                                                    AND a.fecha_eliminacion IS NULL
                                                                )
                                                            )
                                                        ),
                                                        '[]'::json
                                                    )
                                                    FROM estructuras_hijos eh
                                                    WHERE eh.sub_capitulo_id = sc.id
                                                    AND eh.padre_estructura_id IS NULL
                                                ),
                                                'Archivos', (
                                                    SELECT COALESCE(
                                                        json_agg(
                                                            json_build_object(
                                                                'Id', a.id,
                                                                'Nombre', a.nombre_archivo,
                                                                'DescripcionArchivo', a.descripcion_archivo,
                                                                'RutaArchivo', a.ruta_archivo,
                                                                'FechaCarga', a.fecha_carga,
                                                                'TipoArchivo', a.tipo_archivo,
                                                                'FechaCreacionArchivo', a.fecha_creacion,
                                                                'Plano', CASE 
                                                                    WHEN p.id IS NOT NULL THEN json_build_object(
                                                                        'Id', p.id,
                                                                        'Escala', p.escala,
                                                                        'SistemaProyeccion', p.sistema_proyeccion,
                                                                        'NombrePlano', p.nombre_plano,
                                                                        'CodigoPlano', p.codigo_plano,
                                                                        'ColaboradorId', p.colaborador_id,
                                                                        'FechaCreacionPlano', p.fecha_creacion,
                                                                        'Detalles', (
                                                                            SELECT COALESCE(
                                                                                json_agg(
                                                                                    json_build_object(
                                                                                        'DescripcionPlano', pd.descripcion,
                                                                                        'Latitud', pd.latitud_plano,
                                                                                        'Longitud', pd.longitud_plano,
                                                                                        'Area', pd.area
                                                                                    )
                                                                                ),
                                                                                '[]'::json
                                                                            )
                                                                            FROM plano_detalle pd
                                                                            WHERE pd.plano_id = p.id
                                                                        )
                                                                    )
                                                                    ELSE NULL
                                                                END
                                                            )
                                                        ),
                                                        '[]'::json
                                                    )
                                                    FROM archivos a
                                                    LEFT JOIN planos p ON p.archivo_id = a.id AND p.fecha_eliminacion IS NULL
                                                    WHERE a.sub_capitulo_id = sc.id
                                                    AND a.estructura_id IS NULL  -- Solo archivos de subcapítulo
                                                    AND a.fecha_eliminacion IS NULL
                                                )
                                            )
                                        )
                                        FROM subcapitulos sc
                                        WHERE sc.capitulo_id = c.id
                                    )
                                )
                            )
                            FROM capitulos c
                            WHERE c.estudio_ambiental_id = e.id
                        )
                    )
                    FROM estudios_ambientales e
                    WHERE e.id = @id
                    AND e.fecha_eliminacion IS NULL;
                  """;*/

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

    public async Task<EstudioAmbientalCapituloResponse> GetEstudioAmbientalCapitulosByIdAsync(Guid id, CancellationToken cancellationToken)
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

    public async Task<List<EstudioAmbientalEstructuraResponse>> GetEstudioAmbientalEstructurasByIdAsync(Guid id, CancellationToken cancellationToken)
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
                        c.FechaCreacion
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
