using EmployeeService.Application.Contracts.Company;
using EmployeeService.Domain.Exceptions;

namespace EmployeeService.Application.Validators;

public static class CompanyValidator
{
    public static CompanyRequest Validate(this CompanyRequest companyDto)
    {
        ValidatorUtils.WhiteSpaceCheck(companyDto.Name, "Company Name");

        return companyDto;
    }
}