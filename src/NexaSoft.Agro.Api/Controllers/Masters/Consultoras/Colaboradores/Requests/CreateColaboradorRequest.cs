namespace NexaSoft.Agro.Api.Controllers.Masters.Consultoras.Colaboradores.Request;

public sealed record CreateColaboradorRequest(
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
);
