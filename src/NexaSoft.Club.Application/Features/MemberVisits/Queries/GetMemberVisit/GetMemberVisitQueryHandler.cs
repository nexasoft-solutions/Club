using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.MemberVisits;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.MemberVisits.Queries.GetMemberVisit;

public class GetMemberVisitQueryHandler(
    IGenericRepository<MemberVisit> _repository
) : IQueryHandler<GetMemberVisitQuery, MemberVisitResponse>
{
    public async Task<Result<MemberVisitResponse>> Handle(GetMemberVisitQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new MemberVisitSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<MemberVisitResponse>(MemberVisitErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<MemberVisitResponse>(MemberVisitErrores.ErrorConsulta);
        }
    }
}
