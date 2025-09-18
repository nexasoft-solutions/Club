using System.Reflection;
using ClosedXML.Excel;
using NexaSoft.Agro.Application.Abstractions.Excel;
using static NexaSoft.Agro.Domain.Abstractions.ExcelImporter;

namespace NexaSoft.Agro.Infrastructure.Abstractions.Excel;


public class GenericExcelImporter<T> : IGenericExcelImporter<T> where T : new()
{
    public List<T> ImportFromStream(Stream stream)
    {
        var result = new List<T>();

        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheets.First();

        // Obtener propiedades de T con el atributo ExcelColumn
        var properties = typeof(T).GetProperties()
            .Where(p => p.IsDefined(typeof(ExcelColumnAttribute), false))
            .ToList();

        // Crear un diccionario de ColumnName -> propiedad
        var columnMap = properties.ToDictionary(
            prop => prop.GetCustomAttribute<ExcelColumnAttribute>(false)!.NombreColumna,
            prop => prop,
            StringComparer.OrdinalIgnoreCase);

        // Leer la primera fila para saber índices de columnas
        var headerRow = worksheet.Row(1);
        var columnIndexes = new Dictionary<int, PropertyInfo>();

        for (int col = 1; col <= worksheet.LastColumnUsed()!.ColumnNumber(); col++)
        {
            var headerText = headerRow.Cell(col).GetString().Trim();
            if (columnMap.TryGetValue(headerText, out var prop))
            {
                columnIndexes[col] = prop;
            }
        }

        // Recorrer filas desde la fila 2 (asumiendo encabezados en fila 1)
        var lastRow = worksheet.LastRowUsed()!.RowNumber();

        for (int row = 2; row <= lastRow; row++)
        {
            var item = new T();
            bool hasData = false;

            foreach (var colIndex in columnIndexes.Keys)
            {
                var cell = worksheet.Row(row).Cell(colIndex);
                var prop = columnIndexes[colIndex];

                if (cell.IsEmpty())
                    continue;

                var cellValue = cell.GetValue<string>();

                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    hasData = true;

                    object? convertedValue = ConvertToPropertyType(cellValue, prop.PropertyType);

                    prop.SetValue(item, convertedValue);
                }
            }

            if (hasData)
                result.Add(item);
        }

        return result;
    }

    private object? ConvertToPropertyType(string value, Type propertyType)
    {
        if (propertyType == typeof(string))
            return value;

        if (propertyType == typeof(int) || propertyType == typeof(int?))
        {
            if (int.TryParse(value, out int intValue))
                return intValue;
            return null;
        }

        if (propertyType == typeof(long) || propertyType == typeof(long?))
        {
            if (long.TryParse(value, out long longValue))
                return longValue;
            return null;
        }

        if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
        {
            if (decimal.TryParse(value, out decimal decValue))
                return decValue;
            return null;
        }

        if (propertyType == typeof(double) || propertyType == typeof(double?))
        {
            if (double.TryParse(value, out double dblValue))
                return dblValue;
            return null;
        }

        if (propertyType == typeof(bool) || propertyType == typeof(bool?))
        {
            if (bool.TryParse(value, out bool boolValue))
                return boolValue;
            return null;
        }

        if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
        {
            if (DateTime.TryParse(value, out DateTime dateValue))
                return dateValue;
            return null;
        }

        // Agrega más conversiones si es necesario

        return null;
    }
}

