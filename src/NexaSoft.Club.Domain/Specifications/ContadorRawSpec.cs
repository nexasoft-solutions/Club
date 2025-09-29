using NexaSoft.Club.Domain.Masters.Contadores;

namespace NexaSoft.Club.Domain.Specifications;

public class ContadorRawSpec : BaseSpecification<Contador>
{
    public ContadorRawSpec(string entidad, string? prefijo = null)
    {
        if (!string.IsNullOrEmpty(prefijo))
            AddCriteria(c => c.Entidad == entidad && c.Prefijo == prefijo);
        else
            AddCriteria(c => c.Entidad == entidad);
    }
}
