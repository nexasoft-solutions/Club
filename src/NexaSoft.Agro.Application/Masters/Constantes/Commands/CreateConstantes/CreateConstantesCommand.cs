using System.ComponentModel.DataAnnotations;
using static NexaSoft.Agro.Domain.Abstractions.ExcelImporter;

namespace NexaSoft.Agro.Application.Masters.Constantes.Commands.CreateConstantes;

public class CreateConstantesCommand
{
    [ExcelColumn("TipoConstante")]
    [Required(ErrorMessage = "El campo Tipo es obligatorio")]
    public string TipoConstante { get; set; } = "";

    [ExcelColumn("Valor")]
    [Required]
    public string Valor { get; set; } = "";

    [ExcelColumn("UsuarioCreacion")]
    public string UsuarioCreacion { get; set; } = "";
}