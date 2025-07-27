using EmployeeService.Application.Contracts.Department;
using EmployeeService.Domain.Exceptions;

namespace EmployeeService.Application.Validators;

public static class DepartmentValidator
{
    public static AddDepartmentRequest ValidateAdd(this AddDepartmentRequest department, bool companyExists)
    {
        if (!companyExists) throw new CompanyNotFoundException(department.CompanyId);
        ValidatorUtils.WhiteSpaceCheck(department.Name, "Department Name");
        ValidatorUtils.WhiteSpaceCheck(department.Phone, "Department Phone");

        return department;
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
        if (department.Name is not null) ValidatorUtils.WhiteSpaceCheck(department.Name, "Department Name");

        if (department.Phone is not null) ValidatorUtils.WhiteSpaceCheck(department.Phone, "Department Phone");

        return department;
    }
}