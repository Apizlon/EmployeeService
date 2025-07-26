namespace EmployeeService.Application.Contracts;

/// <summary>
/// Компания.
/// </summary>
public class CompanyDto
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