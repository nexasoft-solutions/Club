
namespace NexaSoft.Club.Application.Abstractions.Excel;

public interface IGenericExcelImporter<T>
{
    List<T> ImportFromStream(Stream stream);
}
