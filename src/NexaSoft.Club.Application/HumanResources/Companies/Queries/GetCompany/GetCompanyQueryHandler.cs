using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.Companies;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.Companies.Queries.GetCompany;

public class GetCompanyQueryHandler(
    IGenericRepository<Company> _repository
) : IQueryHandler<GetCompanyQuery, CompanyResponse>
{
    public async Task<Result<CompanyResponse>> Handle(GetCompanyQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new CompanySpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<CompanyResponse>(CompanyErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<CompanyResponse>(CompanyErrores.ErrorConsulta);
        }
    }
}
