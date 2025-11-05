using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

public static class PayrollConceptTypeErrores
{   
     public static readonly Error Duplicado = new(
        "PayrollConceptType.Duplicado",
        "Ya existe una relación entre este tipo de planilla y algunos conceptos seleccionados");
        
    public static readonly Error ErrorSave = new(
        "PayrollConceptType.ErrorSave",
        "Error al guardar las relaciones entre tipo de planilla y conceptos");
}
