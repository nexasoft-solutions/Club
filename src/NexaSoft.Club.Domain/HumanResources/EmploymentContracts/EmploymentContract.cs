using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.HumanResources.ContractTypes;

namespace NexaSoft.Club.Domain.HumanResources.EmploymentContracts;

public class EmploymentContract : Entity
{
    public long? EmployeeId { get; private set; }
    public EmployeeInfo? EmployeeInfo { get; private set; }
    public long? ContractTypeId { get; private set; }
    public ContractType? ContractType { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public decimal Salary { get; private set; }
    public int WorkingHours { get; private set; }
    public string? DocumentPath { get; private set; }

    public bool? IsActive { get; private set; }  
    public int StateEmploymentContract { get; private set; }

    private EmploymentContract() { }

    private EmploymentContract(
        long? employeeId,
        long? contractTypeId,
        DateOnly startDate,
        DateOnly? endDate,
        decimal salary,
        int workingHours,
        string? documentPath,
        bool? isActive,
        int stateEmploymentContract,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        EmployeeId = employeeId;
        ContractTypeId = contractTypeId;
        StartDate = startDate;
        EndDate = endDate;
        Salary = salary;
        WorkingHours = workingHours;
        DocumentPath = documentPath;
        IsActive = isActive;
        StateEmploymentContract = stateEmploymentContract;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static EmploymentContract Create(
        long? employeeId,
        long? contractTypeId,
        DateOnly startDate,
        DateOnly? endDate,
        decimal salary,
        int workingHours,
        string? documentPath,
        bool? isActive,
        int stateEmploymentContract,
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new EmploymentContract(
            employeeId,
            contractTypeId,
            startDate,
            endDate,
            salary,
            workingHours,
            documentPath,
            isActive,
            stateEmploymentContract,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long? employeeId,
        long? contractTypeId,
        DateOnly startDate,
        DateOnly? endDate,
        decimal salary,
        int workingHours,
        bool? isActive,
        string? documentPath,
        DateTime utcNow,
        string? updatedBy
    )
    {
        EmployeeId = employeeId;
        ContractTypeId = contractTypeId;
        StartDate = startDate;
        EndDate = endDate;
        Salary = salary;
        WorkingHours = workingHours;
        DocumentPath = documentPath;
        IsActive = isActive;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateEmploymentContract = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
