using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Queries.GetColaborador;

public class GetColaboradorQueryHandler(
    IColaboradorRepository _repository
) : IQueryHandler<GetColaboradorQuery, ColaboradorResponse>
{
    public async Task<Result<ColaboradorResponse>> Handle(GetColaboradorQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams<int> { Id = query.Id };
            var spec = new ColaboradorSpecification(specParams);

            var (pagination, _) = await _repository.GetColaboradoresAsync(spec, cancellationToken);

            var entity = pagination.Data.FirstOrDefault();

            if (entity is null)
               return Result.Failure<ColaboradorResponse>(ColaboradorErrores.NoEncontrado);

           return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<ColaboradorResponse>(ColaboradorErrores.ErrorConsulta);
        }
    }
}
