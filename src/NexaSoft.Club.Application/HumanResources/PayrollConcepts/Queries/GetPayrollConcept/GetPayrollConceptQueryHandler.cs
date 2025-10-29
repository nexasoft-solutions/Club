using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollConcepts.Queries.GetPayrollConcept;

public class GetPayrollConceptQueryHandler(
    IGenericRepository<PayrollConcept> _repository
) : IQueryHandler<GetPayrollConceptQuery, PayrollConceptResponse>
{
    public async Task<Result<PayrollConceptResponse>> Handle(GetPayrollConceptQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PayrollConceptSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PayrollConceptResponse>(PayrollConceptErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PayrollConceptResponse>(PayrollConceptErrores.ErrorConsulta);
        }
    }
}
