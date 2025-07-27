using System.Text.RegularExpressions;
using EmployeeService.Domain.Exceptions;

namespace EmployeeService.Application.Validators;

public static class ValidatorUtils
{
    public static void EmptyCheck(string? value, string fieldName)
    {
        if (string.IsNullOrEmpty(value))
            throw new BadRequestException($"Error. Field {fieldName} is empty.");
    }

    public static void WhiteSpaceCheck(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BadRequestException($"Error. Field {fieldName} empty or contains whitespaces only.");
    }
    
    public static void DigitsSpacesPlusCheck(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new BadRequestException($"Error. Empty or whitespace {fieldName}.");
        }

        if (!Regex.IsMatch(value, @"^\+?[0-9\s]*$"))
        {
            throw new BadRequestException($"Error. {fieldName} must contain only digits, spaces, or the plus sign (+).");
        }
    }
    
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
}