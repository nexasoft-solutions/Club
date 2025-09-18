using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;

namespace NexaSoft.Agro.Domain.Specifications;

public class ResponsableSpecification : BaseSpecification<Responsable, ResponsableResponse>
{
    public BaseSpecParams SpecParams { get; }

    public ResponsableSpecification(BaseSpecParams<long> specParams) : base()
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
                    case "nombreresponsable":
                        AddCriteria(x => x.NombreResponsable != null && x.NombreResponsable.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "estudioambiental":
                        if (long.TryParse(specParams.Search, out var longx))
                        {
                            AddCriteria(x => x.EstudioAmbientalId == longx);
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


            if (specParams.Ids?.Any() == true)
            {
                AddCriteria(c => specParams.Ids.Contains(c.Id));
            }

            // Aplicar paginación
            if (!specParams.NoPaging)
            {
                ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            }

            // Aplicar ordenamiento
            switch (specParams.Sort)
            {
                case "nombreresponsableasc":
                    AddOrderBy(x => x.NombreResponsable!);
                    break;
                case "nombreresponsabledesc":
                    AddOrderByDescending(x => x.NombreResponsable!);
                    break;
                default:
                    AddOrderBy(x => x.NombreResponsable!);
                    break;
            }
        }

        AddSelect(x => new ResponsableResponse(
               x.Id,
               x.NombreResponsable,
               x.CargoResponsable,
               x.CorreoResponsable,
               x.TelefonoResponsable,
               x.Observaciones,
               x.EstudioAmbientalId,
               x.FechaCreacion,
               x.FechaModificacion,
               x.UsuarioCreacion,
               x.UsuarioModificacion
         ));
    }
}
