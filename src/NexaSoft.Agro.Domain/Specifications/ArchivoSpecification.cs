using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

namespace NexaSoft.Agro.Domain.Specifications;

public class ArchivoSpecification : BaseSpecification<Archivo, ArchivoResponse>
{
    public BaseSpecParams SpecParams { get; }

    public ArchivoSpecification(BaseSpecParams specParams) : base()
    {

        SpecParams = specParams;

        if (specParams.Id.HasValue)
        {
            AddCriteria(x => x.Id == specParams.Id.Value);
        }
        else
        {
            if (!string.IsNullOrEmpty(specParams.Search) && !string.IsNullOrEmpty(specParams.SearchFields))
            {
                switch (specParams.SearchFields.ToLower())
                {
                    case "nombrearchivo":
                        AddCriteria(x => x.NombreArchivo != null && x.NombreArchivo.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "descripcionarchivo":
                        AddCriteria(x => x.DescripcionArchivo != null && x.DescripcionArchivo.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "rutaarchivo":
                        AddCriteria(x => x.RutaArchivo != null && x.RutaArchivo.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "subcapitulo":
                        if (Guid.TryParse(specParams.Search, out var guid))
                        {
                            AddCriteria(x => x.SubCapituloId == guid);
                        }
                        else
                        {
                            // Esto filtra todo y da cero resultados
                            AddCriteria(x => false);
                        }
                        break;
                    case "estructura":
                        if (Guid.TryParse(specParams.Search, out var guidId))
                        {
                            AddCriteria(x => x.EstructuraId == guidId);
                        }
                        else
                        {
                            // Esto filtra todo y da cero resultados
                            AddCriteria(x => false);
                        }
                        break;
                    default:
                        Criteria = x => true;
                        break;
                }
            }


            // Aplicar paginación
            if (!specParams.NoPaging)
            {
                ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            }

            // Aplicar ordenamiento
            switch (specParams.Sort)
            {
                case "nombrearchivoasc":
                    AddOrderBy(x => x.NombreArchivo!);
                    break;
                case "nombrearchivodesc":
                    AddOrderByDescending(x => x.NombreArchivo!);
                    break;
                default:
                    AddOrderBy(x => x.NombreArchivo!);
                    break;
            }
        }

        AddSelect(x => new ArchivoResponse(
               x.Id,
               x.NombreArchivo,
               x.DescripcionArchivo,
               x.RutaArchivo,
               x.FechaCarga,
               null,
               x.EstructuraId,
               x.SubCapituloId,
               x.FechaCreacion,
               x.TipoArchivoId
         ));
    }
}
