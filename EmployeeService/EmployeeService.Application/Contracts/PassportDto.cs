namespace EmployeeService.Application.Contracts;

/// <summary>
/// Паспорт.
/// </summary>
public class PassportDto
{
    /// <summary>
    /// Тип паспорта.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Номер паспорта.
    /// </summary>
    public string? Number { get; set; }
}