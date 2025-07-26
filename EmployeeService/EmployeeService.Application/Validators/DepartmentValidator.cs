using EmployeeService.Application.Contracts.Department;
using EmployeeService.Domain.Exceptions;

namespace EmployeeService.Application.Validators;

public static class DepartmentValidator
{
    public static AddDepartmentRequest ValidateAdd(this AddDepartmentRequest addDepartment, bool companyExists)
    {
        if (!companyExists) throw new CompanyNotFoundException(addDepartment.CompanyId);
        if (string.IsNullOrWhiteSpace(addDepartment.Name))
        {
            throw new BadRequestException("Error. Empty department name.");
        }

        if (string.IsNullOrWhiteSpace(addDepartment.Phone))
        {
            throw new BadRequestException("Error. Empty department phone.");
        }

        return addDepartment;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="department"></param>
    /// <param name="companyExists"></param>
    /// <returns></returns>
    /// <exception cref="CompanyNotFoundException"></exception>
    /// <exception cref="BadRequestException"></exception>
    /// <remarks>Если поле null, оно не обновляется.</remarks>>
    public static UpdateDepartmentRequest ValidateUpdate(this UpdateDepartmentRequest department, bool companyExists)
    {
        if (!companyExists) throw new CompanyNotFoundException(department.CompanyId!.Value);
        if (department.Name is not null && string.IsNullOrWhiteSpace(department.Name))
        {
            throw new BadRequestException("Error. Department name contain only whitespaces.");
        }

        if (department.Phone is not null && string.IsNullOrWhiteSpace(department.Phone))
        {
            throw new BadRequestException("Error. Department phone contain only whitespaces.");
        }

        return department;
    }
}