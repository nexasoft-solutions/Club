using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Companies;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.Companies.Commands.CreateCompany;

public class CreateCompanyCommandHandler(
    IGenericRepository<Company> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateCompanyCommandHandler> _logger
) : ICommandHandler<CreateCompanyCommand, long>
{
    public async Task<Result<long>> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de Company");

     bool existsRuc = await _repository.ExistsAsync(c => c.Ruc == command.Ruc, cancellationToken);
     if (existsRuc)
     {
       return Result.Failure<long>(CompanyErrores.Duplicado);
     }

     bool existsBusinessName = await _repository.ExistsAsync(c => c.BusinessName == command.BusinessName, cancellationToken);
     if (existsBusinessName)
     {
       return Result.Failure<long>(CompanyErrores.Duplicado);
     }

        var entity = Company.Create(
            command.Ruc,
            command.BusinessName,
            command.TradeName,
            command.Address,
            command.Phone,
            command.Website,
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
            _logger.LogInformation("Company con ID {CompanyId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Company");
            return Result.Failure<long>(CompanyErrores.ErrorSave);
        }
    }
}
