namespace EmployeeService.Domain.Exceptions;

/// <summary>
/// Ошибка пользовательского ввода.
/// </summary>
public class BadRequestException : Exception
{
    /// <summary>
    /// Детали ошибки.
    /// </summary>
    public string? Details { get; set; }
    /// <summary>
    /// Вызывает конструктор базового класса.
    /// </summary>
    /// <param name="message">Текст ошибки.</param>
    public BadRequestException(string message) : base(message)
    {
    }
}