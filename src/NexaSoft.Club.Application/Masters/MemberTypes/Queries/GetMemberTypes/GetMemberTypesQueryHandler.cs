using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.MemberTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.MemberTypes.Queries.GetMemberTypes;

public class GetMemberTypesQueryHandler(
    IGenericRepository<MemberType> _repository
) : IQueryHandler<GetMemberTypesQuery, Pagination<MemberTypeResponse>>
{
    public async Task<Result<Pagination<MemberTypeResponse>>> Handle(GetMemberTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new MemberTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<MemberTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<MemberTypeResponse>(
                    query.SpecParams.PageIndex,
                    query.SpecParams.PageSize,
                    totalItems,
                    responses
              );

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<MemberTypeResponse>>(MemberTypeErrores.ErrorConsulta);
        }
    }
}
