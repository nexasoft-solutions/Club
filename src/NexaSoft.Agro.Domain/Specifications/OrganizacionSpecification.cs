using NexaSoft.Agro.Domain.Features.Organizaciones;

namespace NexaSoft.Agro.Domain.Specifications;

public class OrganizacionSpecification : BaseSpecification<Organizacion, OrganizacionResponse>
{
    public BaseSpecParams SpecParams { get; }

    public OrganizacionSpecification(BaseSpecParams<int> specParams) : base()
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
                    case "nombreorganizacion":
                        AddCriteria(x => x.NombreOrganizacion != null && x.NombreOrganizacion.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "telefonocontacto":
                        AddCriteria(x => x.TelefonoContacto != null && x.TelefonoContacto.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    default:
                        Criteria = x => true;
                        break;
                }
            }

            if (specParams.Ids?.Any() == true)
            {
                AddCriteria(c => specParams.Ids.Contains(c.SectorId));
            }

            // Aplicar paginación
            if (!specParams.NoPaging)
            {
                ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            }

            // Aplicar ordenamiento
            switch (specParams.Sort)
            {
                case "nombreorganizacionasc":
                    AddOrderBy(x => x.NombreOrganizacion!);
                    break;
                case "nombreorganizaciondesc":
                    AddOrderByDescending(x => x.NombreOrganizacion!);
                    break;
                default:
                    AddOrderBy(x => x.NombreOrganizacion!);
                    break;
            }
        }

        AddSelect(x => new OrganizacionResponse(
               x.Id,
               x.NombreOrganizacion,
               x.ContactoOrganizacion,
               x.TelefonoContacto,
               x.RucOrganizacion,
               x.Observaciones,
               null,
               x.SectorId,
               x.FechaCreacion,
               x.FechaModificacion,
               x.UsuarioCreacion,
               x.UsuarioModificacion
         ));
    }
}
