namespace NexaSoft.Club.Domain.ServicesModel.Reniec;

public sealed record ReniecRucResponse(
    string direccion,
    string direccion_completa,
    string ruc,
    string nombre_o_razon_social,
    string estado,
    string condicion,
    string departamento,
    string provincia,
    string distrito,
    string ubigeo_sunat,
    string[] ubigeo,
    string es_agente_de_retencion,
    string es_agente_de_percepcion,
    string es_agente_de_percepcion_combustible,
    string es_buen_contribuyente
);
