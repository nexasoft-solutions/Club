using System.ComponentModel.DataAnnotations;
using static NexaSoft.Club.Domain.Abstractions.ExcelImporter;

namespace NexaSoft.Club.Application.Masters.Constantes.Commands.CreateConstantes;

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