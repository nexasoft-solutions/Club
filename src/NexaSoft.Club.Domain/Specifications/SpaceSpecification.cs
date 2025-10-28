using NexaSoft.Club.Domain.Masters.Spaces;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Specifications;

public class SpaceSpecification : BaseSpecification<Space, SpaceResponse>
{
    public BaseSpecParams SpecParams { get; }

    public SpaceSpecification(BaseSpecParams specParams) : base()
    {

        SpecParams = specParams;

        // Incluir relaciones necesarias
        AddInclude(x => x.SpacePhotos.Where(p => p.StateSpacePhoto == (int)EstadosEnum.Activo));
        AddInclude(x => x.SpaceAvailabilities.Where(a => a.StateSpaceAvailability == (int)EstadosEnum.Activo));
        


        if (specParams.Id.HasValue)
        {
            AddCriteria(x => x.Id == specParams.Id.Value);
        }
        else
        {
            if (!string.IsNullOrEmpty(specParams.Search) && !string.IsNullOrEmpty(specParams.SearchFields))
            {
                switch (specParams.SearchFields.ToLower())
                {
                    case "spacename":
                        AddCriteria(x => x.SpaceName != null && x.SpaceName.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "spacetypeid":
                        AddCriteria(x => x.SpaceTypeId == long.Parse(specParams.Search));
                        break;
                }
            }


            // Aplicar paginación
            if (!specParams.NoPaging)
            {
                ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            }

            // Aplicar ordenamiento
            switch (specParams.Sort)
            {
                case "spacenameasc":
                    AddOrderBy(x => x.SpaceName!);
                    break;
                case "spacenamedesc":
                    AddOrderByDescending(x => x.SpaceName!);
                    break;
                default:
                    AddOrderBy(x => x.SpaceName!);
                    break;
            }
        }

         AddSelect(x => new SpaceResponse(
            x.Id,
            x.SpaceTypeId,
            x.SpaceType!.Name!,
            x.SpaceName,
            x.Capacity,
            x.Description,
            x.StandardRate,
            x.RequiresApproval,
            x.MaxReservationHours,
            x.IncomeAccountId,
            x.IncomeAccount!.AccountName!,
            x.CreatedAt,
            x.UpdatedAt,
            x.CreatedBy,
            x.UpdatedBy,
            x.SpacePhotos != null
                ? x.SpacePhotos.Select(photo => new SpacePhotosResponse(
                    photo.Id,
                    photo.SpaceId,
                    photo.PhotoUrl,
                    photo.Order,
                    photo.Description
                )).ToList()
                : new List<SpacePhotosResponse>(),
            x.SpaceAvailabilities != null
                ? x.SpaceAvailabilities.Select(a => new SpaceAvailabilitysResponse(
                    a.Id,
                    a.SpaceId,
                    a.DayOfWeek,
                    a.StartTime,
                    a.EndTime
                )).ToList()
                : new List<SpaceAvailabilitysResponse>()
        ));
    }
}
