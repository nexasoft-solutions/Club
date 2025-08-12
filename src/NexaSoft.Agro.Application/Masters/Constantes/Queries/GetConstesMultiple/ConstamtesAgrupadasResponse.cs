namespace NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstesMultiple;

public sealed record ConstamtesAgrupadasResponse
(
    string Tipo,
    List<ConstanteClaveValorResponse> Valores
);


public sealed record ConstanteClaveValorResponse
(
    int Clave,
    string Valor
);
    