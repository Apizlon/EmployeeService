namespace EmployeeService.Application.Contracts.Department;

/// <summary>
/// Обновить отдел компании.
/// </summary>
public class UpdateDepartmentRequest
{
    /// <summary>
    /// Идентификатор компании.
    /// </summary>
    public int? CompanyId { get; set; }

    /// <summary>
    /// Название отдела.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string? Phone { get; set; }
}