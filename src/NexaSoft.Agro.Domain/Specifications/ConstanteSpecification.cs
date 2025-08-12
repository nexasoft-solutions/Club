using System.Linq.Expressions;
using NexaSoft.Agro.Domain.Masters.Constantes;

namespace NexaSoft.Agro.Domain.Specifications;

public class ConstanteSpecification<TResult> : BaseSpecification<Constante, TResult>
{
    public BaseSpecParams SpecParams { get; }

    public ConstanteSpecification(BaseSpecParams specParams, Expression<Func<Constante, TResult>> selector)
        : base()
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
                    case "tipoconstante":
                        AddCriteria(x => x.TipoConstante != null && x.TipoConstante.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "valor":
                        AddCriteria(x => x.Valor != null && x.Valor.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    default:
                        Criteria = x => true;
                        break;
                }
            }

            if (!specParams.NoPaging)
            {
                ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            }

            // Ordenamiento
            switch (specParams.Sort)
            {
                case "tipoconstanteasc":
                    AddOrderBy(x => x.TipoConstante!);
                    break;
                case "tipoconstantedesc":
                    AddOrderByDescending(x => x.TipoConstante!);
                    break;
                default:
                    AddOrderBy(x => x.TipoConstante!);
                    break;
            }
        }

        AddSelect(selector);
    }
}
