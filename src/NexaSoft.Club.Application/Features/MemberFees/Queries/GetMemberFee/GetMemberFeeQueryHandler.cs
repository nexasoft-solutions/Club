using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.MemberFees;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.MemberFees.Queries.GetMemberFee;

public class GetMemberFeeQueryHandler(
    IGenericRepository<MemberFee> _repository
) : IQueryHandler<GetMemberFeeQuery, MemberFeeResponse>
{
    public async Task<Result<MemberFeeResponse>> Handle(GetMemberFeeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new MemberFeeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<MemberFeeResponse>(MemberFeeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<MemberFeeResponse>(MemberFeeErrores.ErrorConsulta);
        }
    }
}
