using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Companies;

namespace NexaSoft.Club.Application.HumanResources.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommandHandler(
    IGenericRepository<Company> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateCompanyCommandHandler> _logger
) : ICommandHandler<UpdateCompanyCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de Company con ID {CompanyId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Company con ID {CompanyId} no encontrado", command.Id);
                return Result.Failure<bool>(CompanyErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Ruc,
            command.BusinessName,
            command.TradeName,
            command.Address,
            command.Phone,
            command.Website,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Company con ID {CompanyId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar Company con ID {CompanyId}", command.Id);
            return Result.Failure<bool>(CompanyErrores.ErrorEdit);
        }
    }
}
