using NexaSoft.Agro.Domain.Features.Proyectos.Planos;

namespace NexaSoft.Agro.Domain.Specifications;

public class PlanoSpecification : BaseSpecification<Plano, PlanoResponse>
{
    public BaseSpecParams SpecParams { get; }

    public PlanoSpecification(BaseSpecParams specParams) : base()
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
                    case "nombreplano":
                        AddCriteria(x => x.NombrePlano != null && x.NombrePlano.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "archivo":
                        if (long.TryParse(specParams.Search, out var longx))
                        {
                            AddCriteria(x => x.ArchivoId == longx);
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
                case "escalaasc":
                    AddOrderBy(x => x.EscalaId!);
                    break;
                case "escaladesc":
                    AddOrderByDescending(x => x.EscalaId!);
                    break;
                default:
                    AddOrderBy(x => x.EscalaId!);
                    break;
            }
        }

        AddSelect(x => new PlanoResponse(
               x.Id,
               null,
               x.SistemaProyeccion,
               x.NombrePlano,
               x.CodigoPlano,
               x.Archivo!.NombreArchivo!,
               x.Colaborador!.UserName!,
               x.ArchivoId,
               new(),
               x.FechaCreacion,
               x.UsuarioCreacion
         ));
    }
}
