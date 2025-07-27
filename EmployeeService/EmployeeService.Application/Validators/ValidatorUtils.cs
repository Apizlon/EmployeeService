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
}