using System.Text.RegularExpressions;
using EmployeeService.Domain.Exceptions;

namespace EmployeeService.Application.Validators;

/// <summary>
/// Статические методы для валидации.
/// </summary>
public static class ValidatorUtils
{
    /// <summary>
    /// Проверка поля на пустоту
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <param name="fieldName">Название поля.</param>
    /// <exception cref="BadRequestException"><see cref="BadRequestException"/>.</exception>
    public static void EmptyCheck(string? value, string fieldName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new BadRequestException($"Error. Field {fieldName} is empty.");
        }
    }

    /// <summary>
    /// Проверка поля на пробелы.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <param name="fieldName">Название поля.</param>
    /// <exception cref="BadRequestException"><see cref="BadRequestException"/>.</exception>
    public static void WhiteSpaceCheck(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new BadRequestException($"Error. Field {fieldName} empty or contains whitespaces only.");
        }
    }

    /// <summary>
    /// Проверка номера телефона.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <param name="fieldName">Название поля.</param>
    /// <exception cref="BadRequestException"><see cref="BadRequestException"/>.</exception>
    public static void DigitsSpacesPlusCheck(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new BadRequestException($"Error. Empty or whitespace {fieldName}.");
        }

        if (!Regex.IsMatch(value, @"^\+?[0-9\s]*$"))
        {
            throw new BadRequestException(
                $"Error. {fieldName} must contain only digits, spaces, or the plus sign (+).");
        }
    }

    /// <summary>
    /// Проверка, что строка состоит только из цифр и пробелов.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <param name="fieldName">Название поля.</param>
    /// <exception cref="BadRequestException"><see cref="BadRequestException"/>.</exception>
    public static void DigitsAndSpacesCheck(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new BadRequestException($"Error. Empty or whitespace {fieldName}.");
        }

        if (!Regex.IsMatch(value, @"^[0-9\s]*$"))
        {
            throw new BadRequestException($"Error. {fieldName} must contain only digits and spaces.");
        }
    }

    /// <summary>
    /// Проверка длины строки.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <param name="fieldName">Название поля.</param>
    /// <param name="maxLength">Максимальная длина строки.</param>
    /// <exception cref="BadRequestException"><see cref="BadRequestException"/>.</exception>
    public static void LengthCheck(string? value, int maxLength, string fieldName)
    {
        if (value is null)
        {
            throw new BadRequestException($"Error. Field {fieldName} is null.");
        }

        if (value.Length > maxLength)
        {
            throw new BadRequestException($"Error. {fieldName} length is more than {maxLength}.");
        }
    }
}