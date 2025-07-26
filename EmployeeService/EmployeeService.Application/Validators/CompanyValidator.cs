using EmployeeService.Application.Contracts.Company;
using EmployeeService.Domain.Exceptions;

namespace EmployeeService.Application.Validators;

public static class CompanyValidator
{
    public static CompanyRequest Validate(this CompanyRequest companyDto)
    {
        if (string.IsNullOrWhiteSpace(companyDto.Name))
        {
            throw new BadRequestException("Error. Empty company name.");
        }

        return companyDto;
    }
}