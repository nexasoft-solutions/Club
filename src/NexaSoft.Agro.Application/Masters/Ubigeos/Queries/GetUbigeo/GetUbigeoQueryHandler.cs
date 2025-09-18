using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Ubigeos;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Masters.Ubigeos.Queries.GetUbigeo;

public class GetUbigeoQueryHandler(
    IGenericRepository<Ubigeo> _repository
) : IQueryHandler<GetUbigeoQuery, UbigeoResponse>
{
    public async Task<Result<UbigeoResponse>> Handle(GetUbigeoQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams<long> { Id = query.Id };
            var spec = new UbigeoSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<UbigeoResponse>(UbigeoErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<UbigeoResponse>(UbigeoErrores.ErrorConsulta);
        }
    }
}
