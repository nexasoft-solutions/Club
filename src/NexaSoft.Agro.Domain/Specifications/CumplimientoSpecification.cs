using NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;

namespace NexaSoft.Agro.Domain.Specifications;

public class CumplimientoSpecification : BaseSpecification<Cumplimiento, CumplimientoResponse>
{
    public BaseSpecParams SpecParams { get; }

    public CumplimientoSpecification(BaseSpecParams specParams) : base()
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
            case "idasc":
                AddOrderBy(x => x.Id!);
                break;
            case "iddesc":
                AddOrderByDescending(x => x.Id!);
                break;
            default:
                AddOrderBy(x => x.Id!);
                break;
        }
    }

      AddSelect(x => new CumplimientoResponse(
             x.Id,
             x.FechaCumplimiento,
             x.RegistradoaTiempo,
             x.Observaciones,
             x.EventoRegulatorioId,
             x.EventoRegulatorio!.NombreEvento!,
             x.FechaCreacion,
             x.FechaModificacion,
             x.UsuarioCreacion,
             x.UsuarioModificacion
       ));
   }
}
