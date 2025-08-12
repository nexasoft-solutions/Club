using NexaSoft.Agro.Domain.Masters.Contadores;

namespace NexaSoft.Agro.Domain.Specifications;

public class ContadorRawSpec : BaseSpecification<Contador>
{
    public ContadorRawSpec(string entidad, string? agrupador = null)
    {
        if (!string.IsNullOrEmpty(agrupador))
            AddCriteria(c => c.Entidad == entidad && c.Agrupador == agrupador);
        else
            AddCriteria(c => c.Entidad == entidad);
    }
}
