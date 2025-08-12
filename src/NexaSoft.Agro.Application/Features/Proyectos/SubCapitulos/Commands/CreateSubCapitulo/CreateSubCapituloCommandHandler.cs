using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Commands.CreateSubCapitulo;

public class CreateSubCapituloCommandHandler(
    IGenericRepository<SubCapitulo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateSubCapituloCommandHandler> _logger
) : ICommandHandler<CreateSubCapituloCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSubCapituloCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de SubCapitulo");
        var validator = new CreateSubCapituloCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }


        var entity = SubCapitulo.Create(
            command.NombreSubCapitulo,
            command.DescripcionSubCapitulo,
            command.CapituloId,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime()
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("SubCapitulo con ID {SubCapituloId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear SubCapitulo");
            return Result.Failure<Guid>(SubCapituloErrores.ErrorSave);
        }
    }
}
