using EmployeeService.Application.Contracts.Employee;
using EmployeeService.DataAccess.Repositories;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Exceptions;

namespace EmployeeService.Application.Validators;

/// <summary>
/// Валидотор сотрудника.
/// </summary>
public static class EmployeeValidator
{
    /// <summary>
    /// Валидация при добавлении сотрудника.
    /// </summary>
    /// <param name="employee"><see cref="AddEmployeeRequest"/>.</param>
    /// <param name="department"><see cref="Department"/>.</param>
    /// <returns><see cref="AddEmployeeRequest"/>.</returns>
    /// <exception cref="BadRequestException">Ошибка пользовательских даненых.</exception>
    public static AddEmployeeRequest ValidateAdd(this AddEmployeeRequest employee, Department department)
    {
        if (department.CompanyId != employee.CompanyId)
            throw new BadRequestException("Error. The specified company does not have such a department.");
        ValidateName(employee.Name!);
        ValidateName(employee.Surname!);
        ValidateName(employee.Phone!);
        if (employee.Passport is null) throw new BadRequestException("Error. Passport is not provided");
        employee.Passport.ValidateAdd();
        return employee;
    }

    /// <summary>
    /// Валидация при изменении сотрудника.
    /// </summary>
    /// <param name="employee"><see cref="UpdateEmployeeRequest"/>.</param>
    /// <param name="oldEmployee"><see cref="Employee"/>.</param>
    /// <param name="departmentRepository"><see cref="IDepartmentRepository"/>.</param>
    /// <returns><see cref="UpdateEmployeeRequest"/>.</returns>
    /// <remarks>Если поле null, оно не обновляется.</remarks>>
    public static async Task<UpdateEmployeeRequest> ValidateUpdate(this UpdateEmployeeRequest employee,
        Employee oldEmployee, IDepartmentRepository departmentRepository)
    {
        if (employee.Name is not null)
        {
            ValidateName(employee.Name!);
        }

        if (employee.Surname is not null)
        {
            ValidateName(employee.Surname!);
        }

        if (employee.Phone is not null)
        {
            ValidateName(employee.Phone!);
        }

        await employee.ValidateOrgAndDepartmentUpdate(oldEmployee, departmentRepository);
        return employee;
    }

    private static async Task ValidateOrgAndDepartmentUpdate(this UpdateEmployeeRequest employee, Employee oldEmployee,
        IDepartmentRepository departmentRepository)
    {
        if (employee.DepartmentId is null && employee.CompanyId is null) return;
        if (employee.DepartmentId is not null && employee.CompanyId is null)
        {
            var department = await departmentRepository.GetDepartmentAsync(employee.DepartmentId.Value) ??
                             throw new DepartmentNotFoundException(employee.DepartmentId.Value);
            if (department.CompanyId != oldEmployee.CompanyId)
                throw new BadRequestException("Error. The specified department belongs to another company.");
            return;
        }

        if (employee.DepartmentId is null && employee.CompanyId is not null)
            throw new BadRequestException("Error. Impossible to change a company without changing a department.");
        var newDepartment = await departmentRepository.GetDepartmentAsync(employee.DepartmentId!.Value) ??
                            throw new DepartmentNotFoundException(employee.DepartmentId.Value);
        if (newDepartment.CompanyId != employee.CompanyId)
            throw new BadRequestException("Error. The specified company does not have such a department.");
    }

    private static void ValidateName(string name)
    {
        ValidatorUtils.WhiteSpaceCheck(name, "Employee Name");
        ValidatorUtils.LengthCheck(name, 255, "Employee Name");
    }

    private static void ValidateSurname(string surname)
    {
        ValidatorUtils.WhiteSpaceCheck(surname, "Employee Surname");
        ValidatorUtils.LengthCheck(surname, 255, "Employee Surname");
    }

    private static void ValidatePhone(string phone)
    {
        ValidatorUtils.DigitsSpacesPlusCheck(phone, "Employee Phone");
        ValidatorUtils.LengthCheck(phone, 50, "Employee Phone");
    }
}