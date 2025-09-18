using NexaSoft.Agro.Domain.Masters.Constantes;

namespace NexaSoft.Agro.Domain.Specifications;

public class ConstanteMultipleSpecification : BaseSpecification<Constante, ConstanteClaveValorResponse>
{
   public ConstanteMultipleSpecification(List<string>? tipos)
    {
        if (tipos is { Count: > 0 })
        {
            AddCriteria(x => tipos.Contains(x.TipoConstante!));
        }

        AddOrderBy(x => x.TipoConstante!);
        AddOrderBy(x => x.Valor!);

        AddSelect(x => new ConstanteClaveValorResponse(
            x.TipoConstante,
            x.Clave,
            x.Valor ?? ""            
        ));
    }
}
