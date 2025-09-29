using NexaSoft.Club.Domain.Masters.Contadores;

namespace NexaSoft.Club.Application.Masters.Contadores;

public interface IContadorRepository
{
    Task<Contador?> GetByEntidadAsync(string entidad, string? agrupador = null, CancellationToken cancellationToken = default);
    Task UpdateAsync(Contador contador, CancellationToken cancellationToken);
}
