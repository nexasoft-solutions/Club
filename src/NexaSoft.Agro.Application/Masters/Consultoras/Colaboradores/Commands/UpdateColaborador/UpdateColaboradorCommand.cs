using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Commands.UpdateColaborador;

public sealed record UpdateColaboradorCommand(
    Guid Id,
    string? NombresColaborador,
    string? ApellidosColaborador,
    int TipoDocumentoId,
    string? NumeroDocumentoIdentidad,
    DateOnly? FechaNacimiento,
    int GeneroColaboradorId,
    int EstadoCivilColaboradorId,
    string? Direccion,
    string? CorreoElectronico,
    string? TelefonoMovil,
    int CargoId,
    int DepartamentoId,
    DateOnly? FechaIngreso,
    decimal? Salario,
    DateOnly? FechaCese,
    string? Comentarios,
    Guid ConsultoraId
) : ICommand<bool>;
