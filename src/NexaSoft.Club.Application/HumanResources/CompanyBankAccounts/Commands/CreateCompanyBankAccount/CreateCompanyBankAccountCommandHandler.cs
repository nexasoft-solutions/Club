using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Commands.CreateCompanyBankAccount;

public class CreateCompanyBankAccountCommandHandler(
    IGenericRepository<CompanyBankAccount> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateCompanyBankAccountCommandHandler> _logger
) : ICommandHandler<CreateCompanyBankAccountCommand, long>
{
    public async Task<Result<long>> Handle(CreateCompanyBankAccountCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de CompanyBankAccount");

     bool existsBankAccountNumber = await _repository.ExistsAsync(c => c.BankAccountNumber == command.BankAccountNumber, cancellationToken);
     if (existsBankAccountNumber)
     {
       return Result.Failure<long>(CompanyBankAccountErrores.Duplicado);
     }

        var entity = CompanyBankAccount.Create(
            command.CompanyId,
            command.BankId,
            command.BankAccountTypeId,
            command.BankAccountNumber,
            command.CciNumber,
            command.CurrencyId,
            command.IsPrimary,
            command.IsActive,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("CompanyBankAccount con ID {CompanyBankAccountId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear CompanyBankAccount");
            return Result.Failure<long>(CompanyBankAccountErrores.ErrorSave);
        }
    }
}
