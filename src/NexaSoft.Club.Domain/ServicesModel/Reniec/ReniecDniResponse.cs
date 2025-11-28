namespace NexaSoft.Club.Domain.ServicesModel.Reniec;

public sealed record ReniecDniResponse(
    string numero,
    string nombre_completo,
    string Nombres,
    string apellido_paterno,
    string apellido_materno,
    int codigo_verificacion,
    string direccion,
    string direccion_completa,
    string ubigeo_reniec,
    string ubigeo_sunat,
    string[] ubigeo
);
