using EmployeeService.Application.Contracts.Department;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Mappers;

public static class DepartmentMapper
{
    public static Department MapToDomain(this AddDepartmentRequest department) =>
        new()
        {
            CompanyId = department.CompanyId,
            Name = department.Name,
            Phone = department.Phone,
        };

    public static Department MapToDomain(this UpdateDepartmentRequest department, int id = 0) =>
        new()
        {
            Id = id,
            CompanyId = department.CompanyId.GetValueOrDefault(),
            Name = department.Name,
            Phone = department.Phone,
        };


    public static DepartmentResponse MapToDto(this Department department) =>
        new()
        {
            Name = department.Name,
            Phone = department.Phone,
        };
}