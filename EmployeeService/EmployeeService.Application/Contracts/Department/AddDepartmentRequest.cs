namespace EmployeeService.Application.Contracts.Department;

/// <summary>
/// Создать Отдел компании.
/// </summary>
public class AddDepartmentRequest
{
    /// <summary>
    /// Идентификатор компании.
    /// </summary>
    public int CompanyId { get; set; }

    /// <summary>
    /// Название отдела.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string? Phone { get; set; }
}