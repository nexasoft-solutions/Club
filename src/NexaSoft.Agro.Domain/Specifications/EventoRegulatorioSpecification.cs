using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

namespace NexaSoft.Agro.Domain.Specifications;

public class EventoRegulatorioSpecification : BaseSpecification<EventoRegulatorio, EventoRegulatorioResponse>
{
    public BaseSpecParams SpecParams { get; }

    public EventoRegulatorioSpecification(BaseSpecParams specParams) : base()
    {

        SpecParams = specParams;

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
                    case "nombre":
                        AddCriteria(x => x.NombreEvento != null && x.NombreEvento.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "descripcion":
                        AddCriteria(x => x.Descripcion != null && x.Descripcion.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "estudioambiental":
                        if (long.TryParse(specParams.Search, out var longx))
                        {
                            AddCriteria(x => x.EstudioAmbientalId == longx);
                        }
                        else
                        {
                            // Esto filtra todo y da cero resultados
                            AddCriteria(x => false);
                        }
                        break;
                    case "id":
                        if (long.TryParse(specParams.Search, out var longid))
                        {
                            AddCriteria(x => x.Id == longid);
                        }
                        else
                        {
                            // Esto filtra todo y da cero resultados
                            AddCriteria(x => false);
                        }
                        break;
                    default:
                        Criteria = x => true;
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
                case "idasc":
                    AddOrderBy(x => x.Id!);
                    break;
                case "iddesc":
                    AddOrderByDescending(x => x.Id!);
                    break;
                default:
                    AddOrderBy(x => x.Id!);
                    break;
            }
        }

        AddSelect(x => new EventoRegulatorioResponse(
               x.Id,
               x.NombreEvento,
               null,
               null,
               x.FechaExpedición,
               x.FechaVencimiento,
               x.Descripcion,
               x.NotificarDíasAntes,
               x.ResponsableId,
               x.Responsable!.NombreResponsable!,
               null,
               null,
               null,
               x.EstudioAmbientalId,
               x.TipoEventoId,
               x.FrecuenciaEventoId,
               x.EstadoEventoId,
               x.EstudioAmbiental!.Proyecto!,
               x.FechaCreacion,
               x.FechaModificacion,
               x.UsuarioCreacion,
               x.UsuarioModificacion,
               x.Responsables
         ));
    }
}
