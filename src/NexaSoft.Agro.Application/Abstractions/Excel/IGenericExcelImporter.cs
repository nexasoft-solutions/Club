
namespace NexaSoft.Agro.Application.Abstractions.Excel;

public interface IGenericExcelImporter<T>
{
    List<T> ImportFromStream(Stream stream);
}
