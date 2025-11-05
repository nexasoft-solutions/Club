using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Reporting;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.EmploymentContracts;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Queries.GetReceiptPeriodByEmployee;

public class GetReceiptPeriodByEmployeeQueryHandler(
    IGenericRepository<PayrollPeriod> _payrollPeriodRepository,
    IGenericRepository<EmploymentContract> _employmentContractRepository,
    IPayrollReceiptService _receiptPeriodService
) : IQueryHandler<GetReceiptPeriodByEmployeeQuery, byte[]>
{
    public async Task<Result<byte[]>> Handle(GetReceiptPeriodByEmployeeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            // 🔹 Obtener una planilla específica con los detalles del empleado
            var spec = new PayrollPeriodByEmployeeWithDetailSpec(query.PeriodDetailId);
            var periodItem = await _payrollPeriodRepository.GetEntityWithSpec(spec, cancellationToken);

            if (periodItem is null)
                return Result.Failure<byte[]>(PayrollPeriodErrores.NoEncontrado);

            // 🔹 Obtener el empleado asociado al detalle
            var employeeId = periodItem.Details.FirstOrDefault()?.EmployeeId;
            if (employeeId is null)
                return Result.Failure<byte[]>(new Error("Employee.NotFound", "No se encontró el empleado asociado al periodo."));

            // 🔹 Buscar el contrato activo del empleado
            BaseSpecParams sparams = new BaseSpecParams
            {
                Search = employeeId.ToString() ?? string.Empty,
                SearchFields = "employeeid",
                NoPaging = true
            };

            var specContract = new EmploymentContractSpecification(sparams);
            var contracts = await _employmentContractRepository.ListAsync(specContract, cancellationToken);
            var activeContract = contracts.FirstOrDefault(c => c.IsActive ?? false);

            // 🔹 Crear una nueva copia del periodo con los datos de contrato actualizados
            var updatedDetails = periodItem.Details.Select(detail =>
                detail with
                {
                    ContractType = activeContract?.ContractTypeName ?? string.Empty,
                    // Si quieres mostrar también la fecha de inicio del contrato, puedes incluirla aquí
                    // EmployeeHireDate = activeContract?.StartDate
                }
            ).ToList();

            var updatedPeriodItem = periodItem with
            {
                Details = updatedDetails
            };

            // 🔹 Generar PDF del recibo del empleado
            var pdfBytes = _receiptPeriodService.GenerateA5PayrollReceipt(updatedPeriodItem);

            return Result.Success(pdfBytes);
        }
        catch (Exception ex)
        {
            return Result.Failure<byte[]>(new Error("Receipt.GenerationError", $"Error generando comprobante: {ex.Message}"));
        }
    }
}
