namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConcepts.Requests;

public sealed record CreatePayrollConceptsForTypeRequest
(
    long PayrollTypeId,
    List<long> PayrollConceptIds,
    string? CreatedBy
);
