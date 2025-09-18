namespace NexaSoft.Agro.Domain.Abstractions;

public class ExcelImporter
{
    public class ExcelImportResult<T>
    {
        public List<T> Validos { get; set; } = new();
        public List<ExcelImportError> Errores { get; set; } = new();
    }

    public class ExcelImportError
    {
        public int Fila { get; set; }
        public string Mensaje { get; set; } = "";
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelColumnAttribute : Attribute
    {
        public string NombreColumna { get; }

        public ExcelColumnAttribute(string nombreColumna)
        {
            NombreColumna = nombreColumna;
        }
    }
}
