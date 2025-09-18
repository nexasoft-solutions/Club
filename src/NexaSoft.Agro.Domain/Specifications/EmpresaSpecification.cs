using NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;

namespace NexaSoft.Agro.Domain.Specifications;

public class EmpresaSpecification : BaseSpecification<Empresa, EmpresaResponse>
{
    public BaseSpecParams SpecParams { get; }

    public EmpresaSpecification(BaseSpecParams specParams) : base()
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
                    case "razonsocial":
                        AddCriteria(x => x.RazonSocial != null && x.RazonSocial.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "rucempresa":
                        AddCriteria(x => x.RucEmpresa != null && x.RucEmpresa.ToLower().Contains(specParams.Search.ToLower()));
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
                case "razonsocialasc":
                    AddOrderBy(x => x.RazonSocial!);
                    break;
                case "razonsocialdesc":
                    AddOrderByDescending(x => x.RazonSocial!);
                    break;
                default:
                    AddOrderBy(x => x.RazonSocial!);
                    break;
            }
        }

        AddSelect(x => new EmpresaResponse(
               x.Id,
               x.RazonSocial,
               x.RucEmpresa,
               x.ContactoEmpresa,
               x.TelefonoContactoEmpresa,
               x.DepartamentoEmpresa!.Descripcion!,
               x.ProvinciaEmpresa!.Descripcion!,
               x.DistritoEmpresa!.Descripcion!,
               x.Direccion,
               x.LatitudEmpresa,
               x.LongitudEmpresa,
               x.Organizacion!.NombreOrganizacion!,
               x.DepartamentoEmpresaId,
               x.ProvinciaEmpresaId,
               x.DistritoEmpresaId,
               x.OrganizacionId,
               x.FechaCreacion,
               x.FechaModificacion,
               x.UsuarioCreacion,
               x.UsuarioModificacion
         ));
    }
}
