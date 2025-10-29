using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts;

namespace NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Commands.UpdateCompanyBankAccount;

public class UpdateCompanyBankAccountCommandHandler(
    IGenericRepository<CompanyBankAccount> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateCompanyBankAccountCommandHandler> _logger
) : ICommandHandler<UpdateCompanyBankAccountCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateCompanyBankAccountCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de CompanyBankAccount con ID {CompanyBankAccountId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("CompanyBankAccount con ID {CompanyBankAccountId} no encontrado", command.Id);
                return Result.Failure<bool>(CompanyBankAccountErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.CompanyId,
            command.BankId,
            command.BankAccountTypeId,
            command.BankAccountNumber,
            command.CciNumber,
            command.CurrencyId,
            command.IsPrimary,
            command.IsActive,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("CompanyBankAccount con ID {CompanyBankAccountId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar CompanyBankAccount con ID {CompanyBankAccountId}", command.Id);
            return Result.Failure<bool>(CompanyBankAccountErrores.ErrorEdit);
        }
    }
}
