namespace EmployeeService.Application.Contracts.Employee;

/// <summary>
/// DTO для обновления сотрудника.
/// </summary>
public class UpdateEmployeeRequest
{
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
    /// Паспорт.
    /// </summary>
    public PassportDto? Passport { get; set; }

    /// <summary>
    /// Идентификатор компании.
    /// </summary>
    public int? CompanyId { get; set; }

    /// <summary>
    /// Идентификатор отдела компании.
    /// </summary>
    public int? DepartmentId { get; set; }
}