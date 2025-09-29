
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Constantes;
using NexaSoft.Club.Domain.Masters.Contadores;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Masters.Constantes.Commands.CreateConstantes;

public class CreateConstantesBatchCommandHandler(
    IGenericRepository<Constante> _constanteRepository,
    IGenericRepository<Contador> _contadorRepository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateConstantesBatchCommandHandler> _logger
) : ICommandHandler<CreateConstantesBatchCommand, int>
{
    public async Task<Result<int>> Handle(CreateConstantesBatchCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando creación en lote de constantes...");

        var fechaActual = _dateTimeProvider.CurrentTime.ToUniversalTime();
        var constantesToAdd = new List<Constante>();
        var contadoresCache = new Dictionary<string, Contador>();

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            foreach (var cmd in request.Constantes)
            {
                // Validación básica (puedes aplicar Validator aquí si quieres)
                if (string.IsNullOrWhiteSpace(cmd.TipoConstante) || string.IsNullOrWhiteSpace(cmd.Valor))
                    continue;

                // Evitar duplicados
                bool exists = await _constanteRepository.ExistsAsync(c =>
                    c.TipoConstante == cmd.TipoConstante &&
                    c.Valor == cmd.Valor,
                    cancellationToken);

                if (exists)
                    continue;

                // Obtener o crear contador desde cache
                if (!contadoresCache.TryGetValue(cmd.TipoConstante, out var contador))
                {
                    contador = await _contadorRepository.GetEntityWithSpec(
                        new ContadorRawSpec(cmd.TipoConstante), cancellationToken);

                    if (contador == null)
                    {
                        contador = Contador.Create(
                            cmd.TipoConstante,
                            string.Empty,
                            0,
                            string.Empty,
                            "int",
                            0,
                            fechaActual,
                            cmd.UsuarioCreacion
                        );

                        await _contadorRepository.AddAsync(contador, cancellationToken);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                    }

                    contadoresCache[cmd.TipoConstante] = contador;
                }

                var sufijo = DateTime.UtcNow.Year.ToString();
                var nuevoCodigo = contador.Incrementar(fechaActual, cmd.UsuarioCreacion,sufijo);
                await _contadorRepository.UpdateAsync(contador); // sin SaveChanges aún

                var constante = Constante.Create(
                    cmd.TipoConstante,
                    int.Parse(nuevoCodigo),
                    cmd.Valor,
                    (int)EstadosEnum.Activo,
                    fechaActual,
                    cmd.UsuarioCreacion
                );

                constantesToAdd.Add(constante);
            }

            // Insertar en lote
            if (constantesToAdd.Any())
                await _constanteRepository.AddRangeAsync(constantesToAdd, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Carga en lote completada: {Count} registros creados", constantesToAdd.Count);
            return Result.Success(constantesToAdd.Count);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error durante la carga en lote de constantes");
            return Result.Failure<int>(ConstanteErrores.ErrorGuardarEnLote);
        }
    }
}
