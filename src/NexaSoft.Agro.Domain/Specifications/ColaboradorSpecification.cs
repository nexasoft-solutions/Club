using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;

namespace NexaSoft.Agro.Domain.Specifications;

public class ColaboradorSpecification : BaseSpecification<Colaborador, ColaboradorResponse>
{
    public BaseSpecParams SpecParams { get; }

    public ColaboradorSpecification(BaseSpecParams<int> specParams) : base()
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
                   
                    case "nombrecompletocolaborador":
                        AddCriteria(x => x.NombreCompletoColaborador != null && x.NombreCompletoColaborador.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "numerodocumentoidentidad":
                        AddCriteria(x => x.NumeroDocumentoIdentidad != null && x.NumeroDocumentoIdentidad.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "cargoid":
                        if (long.TryParse(specParams.Search, out var searchNumber))
                            AddCriteria(x => x.CargoId == searchNumber);
                        break;
                    default:
                        Criteria = x => true;
                        break;
                }
            }

            if (specParams.Ids?.Any() == true)
            {
                AddCriteria(c => specParams.Ids.Contains(c.DepartamentoId));
            }

            // Aplicar paginación
            if (!specParams.NoPaging)
            {
                ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            }

            // Aplicar ordenamiento
            switch (specParams.Sort)
            {
                case "nombrescolaboradorasc":
                    AddOrderBy(x => x.NombresColaborador!);
                    break;
                case "nombrescolaboradordesc":
                    AddOrderByDescending(x => x.NombresColaborador!);
                    break;
                default:
                    AddOrderBy(x => x.NombresColaborador!);
                    break;
            }
        }

        AddSelect(x => new ColaboradorResponse(
               x.Id,
               x.NombresColaborador,
               x.ApellidosColaborador,
               x.NombreCompletoColaborador,
               null,
               x.NumeroDocumentoIdentidad,
               x.FechaNacimiento,
               null,
               null,
               x.Direccion,
               x.CorreoElectronico,
               x.TelefonoMovil,
               null,
               null,
               x.FechaIngreso,
               x.Salario,
               x.FechaCese,
               x.Comentarios,
               x.ConsultoraId,
               x.Consultora!.NombreConsultora,
               x.UserName,
               x.TipoDocumentoId,
               x.GeneroColaboradorId,
               x.EstadoCivilColaboradorId,
               x.CargoId,
               x.DepartamentoId,
               x.FechaCreacion,
               x.FechaModificacion,
               x.UsuarioCreacion,
               x.UsuarioModificacion
         ));
    }
}
