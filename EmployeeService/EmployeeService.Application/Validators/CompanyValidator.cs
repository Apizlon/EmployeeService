using EmployeeService.Application.Contracts.Company;

namespace EmployeeService.Application.Validators;

/// <summary>
/// Валидотор компании.
/// </summary>
public static class CompanyValidator
{
    /// <summary>
    /// Валидировать.
    /// </summary>
    /// <param name="companyDto"><see cref="CompanyRequest"/>.</param>
    /// <returns><see cref="CompanyRequest"/>.</returns>
    public static CompanyRequest Validate(this CompanyRequest companyDto)
    {
        ValidatorUtils.WhiteSpaceCheck(companyDto.Name, "Company Name");
        ValidatorUtils.LengthCheck(companyDto.Name, 255, "Company Name");

        return companyDto;
    }
}