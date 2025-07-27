using EmployeeService.Application.Contracts;

namespace EmployeeService.Application.Validators;

/// <summary>
/// Валидатор паспорта.
/// </summary>
public static class PassportValidator
{
    /// <summary>
    /// Валидация при добавлении паспорта.
    /// </summary>
    /// <param name="passport"><see cref="PassportDto"/>.</param>
    /// <returns><see cref="PassportDto"/>.</returns>
    public static PassportDto ValidateAdd(this PassportDto passport)
    {
        ValidateType(passport.Type!);
        ValidateNumber(passport.Number!);
        return passport;
    }

    /// <summary>
    /// Валидация при изменении паспорта.
    /// </summary>
    /// <param name="passport"><see cref="PassportDto"/>.</param>
    /// <returns><see cref="PassportDto"/>.</returns>
    /// <remarks>Если поле null, оно не обновляется.</remarks>>
    public static PassportDto ValidateUpdate(this PassportDto passport)
    {
        if (passport.Type is not null)
        {
            ValidateType(passport.Type!);
        }

        if (passport.Number is not null)
        {
            ValidateNumber(passport.Number!);
        }

        return passport;
    }

    private static void ValidateType(string type)
    {
        ValidatorUtils.WhiteSpaceCheck(type, "Passport Type");
        ValidatorUtils.LengthCheck(type, 50, "Passport Type");
    }

    private static void ValidateNumber(string number)
    {
        ValidatorUtils.DigitsAndSpacesCheck(number, "Passport Number");
        ValidatorUtils.LengthCheck(number, 50, "Passport Number");
    }
}