using EmployeeService.Application.Contracts;
using EmployeeService.Application.Contracts.Employee;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Mappers;

public static class EmployeeMapper
{
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