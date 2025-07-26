namespace EmployeeService.Domain.Entities;

/// <summary>
/// Отдел компании.
/// </summary>
public class Department
{
    /// <summary>
    /// Идентификатор отдела.
    /// </summary>
    public int Id { get; set; }

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