using NexaSoft.Agro.Domain.Masters.Consultoras;

namespace NexaSoft.Agro.Domain.Specifications;

public class ConsultoraSpecification : BaseSpecification<Consultora, ConsultoraResponse>
{
    public BaseSpecParams SpecParams { get; }

    public ConsultoraSpecification(BaseSpecParams specParams) : base()
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
                case "nombreconsultora":
                    AddCriteria(x => x.NombreConsultora != null && x.NombreConsultora.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "rucconsultora":
                    AddCriteria(x => x.RucConsultora != null && x.RucConsultora.ToLower().Contains(specParams.Search.ToLower()));
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
            case "nombreconsultoraasc":
                AddOrderBy(x => x.NombreConsultora!);
                break;
            case "nombreconsultoradesc":
                AddOrderByDescending(x => x.NombreConsultora!);
                break;
            default:
                AddOrderBy(x => x.NombreConsultora!);
                break;
        }
    }

      AddSelect(x => new ConsultoraResponse(
             x.Id,
             x.NombreConsultora,
             x.DireccionConsultora,
             x.RepresentanteConsultora,
             x.RucConsultora,
             x.CorreoOrganizacional,
             x.FechaCreacion
       ));
   }
}
