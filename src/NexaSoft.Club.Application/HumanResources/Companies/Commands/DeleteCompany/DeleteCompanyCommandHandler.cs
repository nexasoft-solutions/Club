using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Companies;

namespace NexaSoft.Club.Application.HumanResources.Companies.Commands.DeleteCompany;

public class DeleteCompanyCommandHandler(
    IGenericRepository<Company> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteCompanyCommandHandler> _logger
) : ICommandHandler<DeleteCompanyCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCompanyCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de Company con ID {CompanyId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Company con ID {CompanyId} no encontrado", command.Id);
                return Result.Failure<bool>(CompanyErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar Company con ID {CompanyId}", command.Id);
            return Result.Failure<bool>(CompanyErrores.ErrorDelete);
        }
    }
}
