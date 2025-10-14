using System.Globalization;
using System.Text;

namespace NexaSoft.Club.Domain.Masters.Users;

public static class UserService
{
    public static string CreateFullName(string firstName, string lastName)
    {
        var nombreCompleto = lastName.Trim() + ", " + firstName.Trim();
        return nombreCompleto;
    }

    public static string CreateUserName(string lastName, string firstName)
    {
        if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName))
            return string.Empty;

        var firstNameParts = firstName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var lastNameParts = lastName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (firstNameParts.Length < 1 || lastNameParts.Length < 2)
            throw new ArgumentException("Debe incluir al menos un nombre y dos apellidos");

        var firstNameInitial = firstNameParts[0].ToLower();
        var lastNamePartsInitial = lastNameParts[0].ToLower();
        var lastNamePartsTwoInitial = lastNameParts[1].Substring(0, 1).ToLower();

        // Limpiar caracteres especiales y tildes
        firstNameInitial = CleanText(firstNameInitial);
        lastNamePartsInitial = CleanText(lastNamePartsInitial);
        lastNamePartsTwoInitial = CleanText(lastNamePartsTwoInitial);

        return firstNameInitial + "." + lastNamePartsInitial + lastNamePartsTwoInitial;
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
