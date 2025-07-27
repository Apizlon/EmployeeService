using EmployeeService.Application.Contracts.Employee;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Exceptions;

namespace EmployeeService.Application.Validators;

public static class EmployeeValidator
{
    public static EmployeeRequestDto ValidateAdd(this EmployeeRequestDto employee, Department department)
    {
        if (department.CompanyId != employee.CompanyId) throw new BadRequestException("Error. The specified company does not have such a department.");
        ValidatorUtils.WhiteSpaceCheck(employee.Name, "Employee Name");
        ValidatorUtils.WhiteSpaceCheck(employee.Surname, "Employee Surname");
        ValidatorUtils.WhiteSpaceCheck(employee.Phone, "Employee Phone");
        if (employee.Passport is null) throw new BadRequestException("Error. Passport is not provided");
        employee.Passport.Validate();
        return employee;
    }
}