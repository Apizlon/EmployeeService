using EmployeeService.Application.Contracts;
using EmployeeService.Application.Contracts.Employee;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Mappers;

public static class EmployeeMapper
{
    public static EmployeeResponseDto MapToDto(this Employee employee, Passport passport, Department department) =>
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
    
    public static Employee MapToDomain(this EmployeeRequestDto employee, int passportId) =>
        new()
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            Phone = employee.Phone,
            CompanyId = employee.CompanyId,
            PassportId = passportId,
            DepartmentId = employee.DepartmentId,
        };
}