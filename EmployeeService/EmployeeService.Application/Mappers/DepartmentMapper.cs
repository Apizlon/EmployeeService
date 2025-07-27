using EmployeeService.Application.Contracts.Department;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Mappers;

/// <summary>
/// Маппер сущности отдела компании.
/// </summary>
public static class DepartmentMapper
{
    /// <summary>
    /// Привести к доменной сущности.
    /// </summary>
    /// <param name="department"><see cref="AddDepartmentRequest"/>.</param>
    /// <returns><see cref="Department"/>.</returns>
    public static Department MapToDomain(this AddDepartmentRequest department) =>
        new()
        {
            CompanyId = department.CompanyId,
            Name = department.Name,
            Phone = department.Phone,
        };

    /// <summary>
    /// Привести к доменной сущности.
    /// </summary>
    /// <param name="department"><see cref="UpdateDepartmentRequest"/>.</param>
    /// <param name="id"></param>
    /// <returns><see cref="Department"/>.</returns>
    public static Department MapToDomain(this UpdateDepartmentRequest department, int id = 0) =>
        new()
        {
            Id = id,
            CompanyId = department.CompanyId.GetValueOrDefault(),
            Name = department.Name,
            Phone = department.Phone,
        };


    /// <summary>
    /// Привести к DTO.
    /// </summary>
    /// <param name="department"><see cref="Department"/>.</param>
    /// <returns><see cref="DepartmentResponse"/>.</returns>
    public static DepartmentResponse MapToDto(this Department department) =>
        new()
        {
            Name = department.Name,
            Phone = department.Phone,
        };
}