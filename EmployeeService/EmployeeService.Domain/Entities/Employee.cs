namespace EmployeeService.Domain.Entities;

/// <summary>
/// Сотрудник.
/// </summary>
public class Employee
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
    /// Идентификатор паспорта.
    /// </summary>
    public int PassportId { get; set; }

    /// <summary>
    /// Идентификатор отдела компании.
    /// </summary>
    public int DepartmentId { get; set; }
}