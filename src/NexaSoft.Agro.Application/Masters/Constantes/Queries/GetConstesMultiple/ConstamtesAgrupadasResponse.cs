namespace NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstesMultiple;

public sealed record ConstantesAgrupadasResponse
(
    string Tipo,
    List<ConstantesClaveValorResponse> Valores
);


public sealed record ConstantesClaveValorResponse
(
    int Clave,
    string Valor
);
    