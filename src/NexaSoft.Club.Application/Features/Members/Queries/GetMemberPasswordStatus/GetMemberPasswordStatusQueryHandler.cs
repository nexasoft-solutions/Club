using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Application.Features.Members.Queries.GetMemberPasswordStatus;

public class GetMemberPasswordStatusQueryHandler(
    IGenericRepository<Member> _memberRepository,
    ILogger<GetMemberPasswordStatusQueryHandler> _logger
) : IQueryHandler<GetMemberPasswordStatusQuery, bool>
{
    public async Task<Result<bool>> Handle(GetMemberPasswordStatusQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validando estado de password para DNI: {Dni}", query.Dni);

        try
        {

            bool existsValor = await _memberRepository.ExistsAsync(c => c.Dni == query.Dni && c.BirthDate == query.BirthDate && c.HasSetPassword, cancellationToken);
            if (!existsValor)
            {
                return Result.Failure<bool>(MemberErrores.ErrorHasPassword);
            }


            _logger.LogInformation("Miembro tiene password creado para DNI: {Dni}", query.Dni);
            return Result.Success(existsValor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener metricas del member con ID: {MemberId}", query.Dni);
            return Result.Failure<bool>(MemberErrores.ErrorDataMetrics);
        }
    }
}
