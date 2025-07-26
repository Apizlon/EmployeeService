namespace EmployeeService.Domain.Entities;

/// <summary>
/// Компания.
/// </summary>
public class Company
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название компании.
    /// </summary>
    public string? Name { get; set; }
}