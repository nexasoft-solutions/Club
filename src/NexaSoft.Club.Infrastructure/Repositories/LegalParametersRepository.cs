using NexaSoft.Club.Application.HumanResources.LegalParameters;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.LegalParameters;

namespace NexaSoft.Club.Infrastructure.Repositories;

public class LegalParametersRepository(
    IGenericRepository<LegalParameter> _repository
) : ILegalParametersRepository
{

   public async Task<decimal> GetParameterValue(string code, DateOnly date)
    {
        var parameter = await _repository.FirstOrDefaultAsync(
            p => p.Code == code && 
                 p.EffectiveDate <= date && 
                 (p.EndDate == null || p.EndDate >= date),
            cancellationToken: default);

        return parameter?.Value ?? throw new Exception($"Parámetro legal no encontrado: {code}");
    }

    public async Task<decimal> GetCurrentParameterValue(string code)
    {
        return await GetParameterValue(code, DateOnly.FromDateTime(DateTime.Now));
    }

    public async Task<Dictionary<string, decimal>> GetParametersByCategory(string category, DateOnly date)
    {
        var parameters = await _repository.ListAsync(
            p => p.Category == category &&
                 p.EffectiveDate <= date && 
                 (p.EndDate == null || p.EndDate >= date),
            cancellationToken: default);

        return parameters.ToDictionary(p => p.Code!, p => p.Value);
    }
}