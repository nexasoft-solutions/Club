namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.SubCapitulos.Request;

public sealed record CreateSubCapituloRequest(
    string? NombreSubCapitulo,
    string? DescripcionSubCapitulo,
    Guid CapituloId
);
