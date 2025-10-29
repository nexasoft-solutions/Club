using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.ConceptTypePayrolls;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Queries.GetConceptTypePayroll;

public class GetConceptTypePayrollQueryHandler(
    IGenericRepository<ConceptTypePayroll> _repository
) : IQueryHandler<GetConceptTypePayrollQuery, ConceptTypePayrollResponse>
{
    public async Task<Result<ConceptTypePayrollResponse>> Handle(GetConceptTypePayrollQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new ConceptTypePayrollSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<ConceptTypePayrollResponse>(ConceptTypePayrollErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<ConceptTypePayrollResponse>(ConceptTypePayrollErrores.ErrorConsulta);
        }
    }
}
