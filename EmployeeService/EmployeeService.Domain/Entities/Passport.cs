namespace EmployeeService.Domain.Entities;

/// <summary>
/// Паспорт.
/// </summary>
public class Passport
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор сотрудника.
    /// </summary>
    public int EmployeeId { get; set; } 
    
    /// <summary>
    /// Тип паспорта.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Номер паспорта.
    /// </summary>
    public string? Number { get; set; }
}