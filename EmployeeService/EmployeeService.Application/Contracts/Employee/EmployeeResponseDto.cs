using EmployeeService.Application.Contracts.Department;

namespace EmployeeService.Application.Contracts.Employee;

/// <summary>
/// DTO для получения сотрудника.
/// </summary>
public class EmployeeResponseDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Идентификатор компании.
    /// </summary>
    public int CompanyId { get; set; }

    /// <summary>
    /// Паспорт.
    /// </summary>
    public PassportDto? Passport { get; set; }

    /// <summary>
    /// Отдел компании.
    /// </summary>
    public DepartmentResponseDto? Department { get; set; }
}