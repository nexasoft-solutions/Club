using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores;

public interface IColaboradorRepository
{
   Task<(Pagination<ColaboradorResponse> Items, int TotalItems)> GetColaboradoresAsync(ISpecification<Colaborador> spec, CancellationToken cancellationToken);
}
