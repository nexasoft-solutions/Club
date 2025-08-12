using System.Globalization;
using System.Text;

namespace NexaSoft.Agro.Domain.Masters.Users;

public static class UserService
{
    public static string CreateNombreCompleto(string apellidos, string nombres)
    {
        var nombreCompleto = apellidos.Trim() + ", " + nombres.Trim();
        return nombreCompleto;
    }

    public static string CreateUserName(string apellidos, string nombres)
    {
        if (string.IsNullOrWhiteSpace(apellidos) || string.IsNullOrWhiteSpace(nombres))
            return string.Empty;

        var nombresParts = nombres.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var apellidosParts = apellidos.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (nombresParts.Length < 1 || apellidosParts.Length < 2)
            throw new ArgumentException("Debe incluir al menos un nombre y dos apellidos");

        var inicialNombre = nombresParts[0].Substring(0, 1).ToLower();
        var apellidoPaterno = apellidosParts[0].ToLower();
        var inicialApellidoMaterno = apellidosParts[1].Substring(0, 1).ToLower();

        // Limpiar caracteres especiales y tildes
        inicialNombre = CleanText(inicialNombre);
        apellidoPaterno = CleanText(apellidoPaterno);
        inicialApellidoMaterno = CleanText(inicialApellidoMaterno);

        return inicialNombre + apellidoPaterno + inicialApellidoMaterno;
    }

    private static string CleanText(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var normalized = input.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var c in normalized)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                if (c == 'ñ' || c == 'Ñ')
                    sb.Append('n');
                else
                    sb.Append(c);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC).ToLower();
    }
}
