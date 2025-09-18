namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.SubCapitulos.Request;

public sealed record UpdateSubCapituloRequest(
   long Id,
    string? NombreSubCapitulo,
    string? DescripcionSubCapitulo,
    long CapituloId,
    string? UsuarioModificacion
);
