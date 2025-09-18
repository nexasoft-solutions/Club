namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.SubCapitulos.Request;

public sealed record CreateSubCapituloRequest(
    string? NombreSubCapitulo,
    string? DescripcionSubCapitulo,
    long CapituloId,
    string? UsuarioCreacion
);
