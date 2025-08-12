using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Queries.GetColaboradores;

public class GetColaboradoresQueryHandler(
    IColaboradorRepository _repository
) : IQueryHandler<GetColaboradoresQuery, Pagination<ColaboradorResponse>>
{
    public async Task<Result<Pagination<ColaboradorResponse>>> Handle(GetColaboradoresQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ColaboradorSpecification(query.SpecParams);
            var (pagination, _) = await _repository.GetColaboradoresAsync(spec, cancellationToken);

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<ColaboradorResponse>>(ColaboradorErrores.ErrorConsulta);
        }
    }
}
