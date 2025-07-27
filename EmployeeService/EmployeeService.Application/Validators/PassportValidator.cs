using EmployeeService.Application.Contracts;

namespace EmployeeService.Application.Validators;

public static class PassportValidator
{
    public static PassportDto Validate(this PassportDto passport)
    {
        ValidatorUtils.WhiteSpaceCheck(passport.Type, "Passport Type");
        ValidatorUtils.WhiteSpaceCheck(passport.Number, "Passport Number");
        return passport;
    }
}