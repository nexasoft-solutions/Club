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
                    case "description":
                        AddCriteria(x => x.Description != null && x.Description.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "level":
                        if (long.TryParse(specParams.Search, out var searchNumber))
                            AddCriteria(x => x.Level == searchNumber);
                        break;
                    case "parent":
                        if (long.TryParse(specParams.Search, out var longx))
                        {
                            AddCriteria(x => x.ParentId == longx);
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
                AddCriteria(c => specParams.Ids.Contains(c.ParentId ?? 0));
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
                case "descriptionasc":
                    AddOrderBy(x => x.Description!);
                    break;
                case "descriptiondesc":
                    AddOrderByDescending(x => x.Description!);
                    break;
                default:
                    AddOrderBy(x => x.Description!);
                    break;
            }
        }

        AddSelect(x => new UbigeoResponse(
               x.Id,
               x.Description,
               ((UbigeosEnum)x.Level).ToString(),
               x.ParentId,
               x.Parent!.Description,
               x.CreatedAt,
               x.UpdatedAt,
               x.CreatedBy,
               x.UpdatedBy
         ));
    }
}
