using NexaSoft.Club.Domain.Masters.Ubigeos;
using static NexaSoft.Club.Domain.Shareds.Enums;


namespace NexaSoft.Club.Domain.Specifications;

public class UbigeoSpecification : BaseSpecification<Ubigeo, UbigeoResponse>
{
    public BaseSpecParams SpecParams { get; }

    public UbigeoSpecification(BaseSpecParams<long> specParams) : base()
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
                    case "descripcion":
                        AddCriteria(x => x.Descripcion != null && x.Descripcion.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "nivel":
                        if (long.TryParse(specParams.Search, out var searchNumber))
                            AddCriteria(x => x.Nivel == searchNumber);
                        break;
                    case "padre":
                        if (long.TryParse(specParams.Search, out var longx))
                        {
                            AddCriteria(x => x.PadreId == longx);
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
                AddCriteria(c => specParams.Ids.Contains(c.PadreId ?? 0));
            }

            /*if (specParams.PadreId.HasValue)
            {
                AddCriteria(x => x.PadreId == specParams.PadreId.Value);
            }*/

            // Aplicar paginación
            if (!specParams.NoPaging)
            {
                ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            }

            // Aplicar ordenamiento
            switch (specParams.Sort)
            {
                case "descripcionasc":
                    AddOrderBy(x => x.Descripcion!);
                    break;
                case "descripciondesc":
                    AddOrderByDescending(x => x.Descripcion!);
                    break;
                default:
                    AddOrderBy(x => x.Descripcion!);
                    break;
            }
        }

        AddSelect(x => new UbigeoResponse(
               x.Id,
               x.Descripcion,
               ((UbigeosEnum)x.Nivel).ToString(),
               x.PadreId,
               x.Padre!.Descripcion,
               x.CreatedAt,
               x.UpdatedAt,
               x.CreatedBy,
               x.UpdatedBy
         ));
    }
}
