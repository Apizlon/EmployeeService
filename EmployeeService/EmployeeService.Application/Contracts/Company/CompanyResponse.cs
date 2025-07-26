namespace EmployeeService.Application.Contracts.Company;

/// <summary>
/// Компания.
/// </summary>
public class CompanyResponse
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