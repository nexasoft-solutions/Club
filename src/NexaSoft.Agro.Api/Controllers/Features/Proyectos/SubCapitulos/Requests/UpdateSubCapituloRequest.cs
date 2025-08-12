namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.SubCapitulos.Request;

public sealed record UpdateSubCapituloRequest(
   Guid Id,
    string? NombreSubCapitulo,
    string? DescripcionSubCapitulo,
    Guid CapituloId
);
