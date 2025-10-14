using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Application.Features.Members.Queries.GetMemberPasswordStatus;

public class GetMemberPasswordStatusQueryHandler(
    IGenericRepository<User> _userRepository,
    ILogger<GetMemberPasswordStatusQueryHandler> _logger
) : IQueryHandler<GetMemberPasswordStatusQuery, bool>
{
    public async Task<Result<bool>> Handle(GetMemberPasswordStatusQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validando estado de password para DNI: {Dni}", query.Dni);

        try
        {

            bool existsValor = await _userRepository.ExistsAsync(c => c.Dni == query.Dni && c.BirthDate == query.BirthDate && c.HasSetPassword, cancellationToken);
            if (!existsValor)
            {
                return Result.Failure<bool>(UserErrores.ErrorHasPassword);
            }


            _logger.LogInformation("Usuario tiene password creado para DNI: {Dni}", query.Dni);
            return Result.Success(existsValor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al validar si usuario tiene password con ID: {UserId}", query.Dni);
            return Result.Failure<bool>(UserErrores.ErrorHasPassword);
        }
    }
}
