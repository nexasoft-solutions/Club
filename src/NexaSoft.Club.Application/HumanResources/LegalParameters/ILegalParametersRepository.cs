using NexaSoft.Club.Domain.HumanResources.LegalParameters;

namespace NexaSoft.Club.Application.HumanResources.LegalParameters;

public interface ILegalParametersRepository
{
    Task<decimal> GetParameterValue(string code, DateOnly date);
    Task<decimal> GetCurrentParameterValue(string code);
    Task<Dictionary<string, decimal>> GetParametersByCategory(string category, DateOnly date);

}
