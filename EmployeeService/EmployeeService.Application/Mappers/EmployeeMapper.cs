using EmployeeService.Application.Contracts;
using EmployeeService.Application.Contracts.Employee;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Mappers;

/// <summary>
/// Маппер сущность сотрудника.
/// </summary>
public static class EmployeeMapper
{
    /// <summary>
    /// Привести к DTO.
    /// </summary>
    /// <param name="employee"><see cref="Employee"/>.</param>
    /// <param name="passport"><see cref="Passport"/>.</param>
    /// <param name="department"><see cref="Department"/>.</param>
    /// <returns><see cref="EmployeeResponse"/>.</returns>
    public static EmployeeResponse MapToDto(this Employee employee, Passport passport, Department department) =>
        new()
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            Phone = employee.Phone,
            CompanyId = employee.CompanyId,
            Passport = new PassportDto()
            {
                Type = passport.Type,
                Number = passport.Number,
            },
            Department = department.MapToDto(),
        };

    /// <summary>
    /// Привести к доменной сущности.
    /// </summary>
    /// <param name="employee"><see cref="AddEmployeeRequest"/>.</param>
    /// <param name="passportId">Идентификатор паспорта.</param>
    /// <returns><see cref="Employee"/>.</returns>
    public static Employee MapToDomain(this AddEmployeeRequest employee, int passportId) =>
        new()
        {
            Name = employee.Name,
            Surname = employee.Surname,
            Phone = employee.Phone,
            CompanyId = employee.CompanyId,
            PassportId = passportId,
            DepartmentId = employee.DepartmentId,
        };

    /// <summary>
    /// Привести к доменной сущности(обновление).
    /// </summary>
    /// <param name="employee"><see cref="UpdateEmployeeRequest"/>.</param>
    /// <returns><see cref="Employee"/>.</returns>
    public static Employee MapToDomain(this UpdateEmployeeRequest employee) =>
        new()
        {
            Name = employee.Name,
            Surname = employee.Surname,
            Phone = employee.Phone,
            CompanyId = employee.CompanyId ?? 0,
            PassportId = 0,
            DepartmentId = employee.DepartmentId ?? 0,
        };
}