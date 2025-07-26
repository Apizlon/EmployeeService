namespace EmployeeService.Application.Contracts.Department;

/// <summary>
/// Отдел компании.
/// </summary>
public class DepartmentResponseDto
{
    /// <summary>
    /// Название отдела.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string? Phone { get; set; }
}