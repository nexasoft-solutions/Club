using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

namespace NexaSoft.Club.Domain.Specifications;

public class PayrollPeriodByEmployeeWithDetailSpec
    : BaseSpecification<PayrollPeriod, PayrollPeriodItemResponse>
{
 public PayrollPeriodByEmployeeWithDetailSpec(long periodDetailId) : base()
    {
      
        // 🔹 Filtra por empleado dentro del periodo
        AddCriteria(p => p.PayrollDetails.Any(d => d.Id == periodDetailId));
    
        // 🔹 Selecciona UNA planilla con los detalles del empleado y sus conceptos
        AddSelect(p => new PayrollPeriodItemResponse(
            p.Id,
            p.PeriodName,
            p.PayrollTypeId,
            p.PayrollType != null ? p.PayrollType.Code! : string.Empty,
            p.StartDate,
            p.EndDate,
            p.TotalAmount,
            p.TotalEmployees,
            p.StatusId,
            p.CreatedAt,
            p.CreatedBy,
            p.PayrollDetails
                .Where(d => d.Id == periodDetailId)
                .Select(d => new PayrollDetailItemsResponse(
                    d.Id,
                    p.PeriodName,
                    DateOnly.FromDateTime(d.CreatedAt),
                    d.EmployeeId,
                    d.Employee!.EmployeeCode!,
                    d.Employee!.User!.FullName!,
                    d.Employee!.User!.Dni!,
                    d.Employee!.Position!.Code!,
                    d.Employee!.Department!.Code!,
                    string.Empty,
                    d.Employee!.HireDate!,
                    d.BaseSalary ?? 0,
                    d.TotalIncome ?? 0,
                    d.TotalDeductions ?? 0,
                    d.NetPay ?? 0,
                    d.PayrollDetailConcepts.Select(c => new PayrollDetailConceptItemsResponse(
                        c.Id,
                        c.ConceptId,
                        c.Concept != null ? c.Concept.Code! : string.Empty,
                        c.Concept != null ? c.Concept.Name! : string.Empty,
                        c.Concept!.ConceptTypePayroll!.Code!,
                        c.Amount ?? 0,
                        c.Quantity ?? 0,
                        c.CalculatedValue ?? 0,
                        c.Description
                    )).ToList()
                )).ToList()
        ));
    }
}
