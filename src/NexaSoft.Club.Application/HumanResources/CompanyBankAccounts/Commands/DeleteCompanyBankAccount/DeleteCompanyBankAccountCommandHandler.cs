using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts;

namespace NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Commands.DeleteCompanyBankAccount;

public class DeleteCompanyBankAccountCommandHandler(
    IGenericRepository<CompanyBankAccount> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteCompanyBankAccountCommandHandler> _logger
) : ICommandHandler<DeleteCompanyBankAccountCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCompanyBankAccountCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de CompanyBankAccount con ID {CompanyBankAccountId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("CompanyBankAccount con ID {CompanyBankAccountId} no encontrado", command.Id);
                return Result.Failure<bool>(CompanyBankAccountErrores.NoEncontrado);
            }

         entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(),command.DeletedBy);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al eliminar CompanyBankAccount con ID {CompanyBankAccountId}", command.Id);
            return Result.Failure<bool>(CompanyBankAccountErrores.ErrorDelete);
        }
    }
}
