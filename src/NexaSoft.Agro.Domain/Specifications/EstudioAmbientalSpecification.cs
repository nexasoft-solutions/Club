using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

namespace NexaSoft.Agro.Domain.Specifications;

public class EstudioAmbientalSpecification : BaseSpecification<EstudioAmbiental, EstudioAmbientalResponse>
{
    public BaseSpecParams SpecParams { get; }

    public EstudioAmbientalSpecification(BaseSpecParams specParams) : base()
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
                    case "proyecto":
                        AddCriteria(x => x.Proyecto != null && x.Proyecto.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "empresa":
                        if (Guid.TryParse(specParams.Search, out var guid))
                        {
                            AddCriteria(x => x.EmpresaId == guid);
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
                case "proyectoasc":
                    AddOrderBy(x => x.Proyecto!);
                    break;
                case "proyectodesc":
                    AddOrderByDescending(x => x.Proyecto!);
                    break;
                default:
                    AddOrderBy(x => x.Proyecto!);
                    break;
            }
        }

        AddSelect(x => new EstudioAmbientalResponse(
               x.Id,
               x.Proyecto,
               x.FechaInicio,
               x.FechaFin,
               x.Detalles,
               null,
               x.Empresa!.RazonSocial!,
               x.EmpresaId,
               x.CodigoEstudio,
               x.FechaCreacion
         ));
    }
}
