using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.EmploymentContracts;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Queries.GetPayrollPeriodByEmployee;

public class GetPayrollPeriodByEmployeeQueryHandler(
    IGenericRepository<PayrollPeriod> _repository,
    IGenericRepository<EmploymentContract> _employmentContractRepository
) : IQueryHandler<GetPayrollPeriodByEmployeeQuery, List<PayrollPeriodItemResponse>>
{
    public async Task<Result<List<PayrollPeriodItemResponse>>> Handle(GetPayrollPeriodByEmployeeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            // 🔹 Buscar contratos activos del empleado
            BaseSpecParams sparams = new BaseSpecParams
            {
                Search = query.EmployeeId.ToString(),
                SearchFields = "employeeid",
                NoPaging = true
            };

            var specContract = new EmploymentContractSpecification(sparams);
            var contracts = await _employmentContractRepository.ListAsync(specContract, cancellationToken);

            // 🔹 Buscar planillas del empleado con detalles y conceptos
            var spec = new PayrollPeriodByEmployeeWithConceptsSpec(query.EmployeeId);
            var payrolls = await _repository.ListAsync(spec, cancellationToken);

            if (payrolls is null || !payrolls.Any())
                return Result.Failure<List<PayrollPeriodItemResponse>>(PayrollPeriodErrores.NoEncontrado);

            // 🔹 Crear nuevas copias inmutables con el tipo de contrato asignado
            var updatedPayrolls = payrolls.Select(payroll =>
            {
                var updatedDetails = payroll.Details.Select(detail =>
                {
                    var contract = contracts.FirstOrDefault(c => c.IsActive ?? false);

                    // Crear nueva copia del detalle con ContractType actualizado
                    return detail with
                    {
                        //EmployeeHireDate = contract?.StartDate,
                        ContractType = contract?.ContractTypeName ?? string.Empty
                    };
                }).ToList();

                // Crear nueva copia del payroll con la lista actualizada
                return payroll with
                {
                    Details = updatedDetails
                };
            }).ToList();

            return Result.Success(updatedPayrolls);
        }
        catch (Exception ex)
        {
            // 🔹 Registrar error (puedes loguear si deseas)
            var errores = ex.Message;
            return Result.Failure<List<PayrollPeriodItemResponse>>(PayrollPeriodErrores.ErrorConsulta);
        }
    }
}
