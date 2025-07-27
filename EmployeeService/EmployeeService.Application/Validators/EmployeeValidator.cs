using EmployeeService.Application.Contracts.Employee;
using EmployeeService.DataAccess.Repositories;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Exceptions;

namespace EmployeeService.Application.Validators;

public static class EmployeeValidator
{
    public static AddEmployeeRequest ValidateAdd(this AddEmployeeRequest employee, Department department)
    {
        if (department.CompanyId != employee.CompanyId)
            throw new BadRequestException("Error. The specified company does not have such a department.");
        ValidatorUtils.WhiteSpaceCheck(employee.Name, "Employee Name");
        ValidatorUtils.WhiteSpaceCheck(employee.Surname, "Employee Surname");
        ValidatorUtils.DigitsSpacesPlusCheck(employee.Phone, "Employee Phone");
        if (employee.Passport is null) throw new BadRequestException("Error. Passport is not provided");
        employee.Passport.ValidateAdd();
        return employee;
    }
    
    public static async Task<UpdateEmployeeRequest> ValidateUpdate(this UpdateEmployeeRequest employee, Employee oldEmployee, IDepartmentRepository departmentRepository)
    {
        if (employee.Name is not null) ValidatorUtils.WhiteSpaceCheck(employee.Name, "Employee Name");
        if (employee.Surname is not null) ValidatorUtils.WhiteSpaceCheck(employee.Surname, "Employee Surname");
        if (employee.Phone is not null) ValidatorUtils.DigitsSpacesPlusCheck(employee.Phone, "Employee Phone");
        await employee.ValidateOrgAndDepartmentUpdate(oldEmployee, departmentRepository);
        return employee;
    }

    private static async Task ValidateOrgAndDepartmentUpdate(this UpdateEmployeeRequest employee, Employee oldEmployee, IDepartmentRepository departmentRepository)
    {
        if (employee.DepartmentId is null && employee.CompanyId is null) return;
        if (employee.DepartmentId is not null && employee.CompanyId is null)
        {
            var department = await departmentRepository.GetDepartment(employee.DepartmentId.Value) ?? throw new DepartmentNotFoundException(employee.DepartmentId.Value);
            if (department.CompanyId != oldEmployee.CompanyId) throw new BadRequestException("Error. The specified department belongs to another company.");
            return;
        }
        if (employee.DepartmentId is null && employee.CompanyId is not null) throw new BadRequestException("Error. Impossible to change a company without changing a department.");
        var newDepartment = await departmentRepository.GetDepartment(employee.DepartmentId!.Value) ?? throw new DepartmentNotFoundException(employee.DepartmentId.Value);
        if (newDepartment.CompanyId != employee.CompanyId) throw new BadRequestException("Error. The specified company does not have such a department.");
    }
}