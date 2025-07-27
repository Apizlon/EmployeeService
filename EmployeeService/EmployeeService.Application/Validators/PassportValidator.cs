using EmployeeService.Application.Contracts;

namespace EmployeeService.Application.Validators;

public static class PassportValidator
{
    public static PassportDto ValidateAdd(this PassportDto passport)
    {
        ValidatorUtils.WhiteSpaceCheck(passport.Type, "Passport Type");
        ValidatorUtils.DigitsAndSpacesCheck(passport.Number, "Passport Number");
        return passport;
    }

    public static PassportDto ValidateUpdate(this PassportDto passport)
    {
        if (passport.Type is not null) ValidatorUtils.WhiteSpaceCheck(passport.Type, "Passport Type");
        if (passport.Number is not null) ValidatorUtils.DigitsAndSpacesCheck(passport.Number, "Passport Number");
        return passport;
    }
}