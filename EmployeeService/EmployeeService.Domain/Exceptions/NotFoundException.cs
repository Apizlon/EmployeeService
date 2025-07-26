namespace EmployeeService.Domain.Exceptions;

/// <summary>
/// Ошибка: не найден.
/// </summary>
public abstract class NotFoundException : Exception
{
    /// <summary>
    /// Вызывает конструктор базового класса.
    /// </summary>
    /// <param name="message">Текст ошибки.</param>
    protected NotFoundException(string message) : base(message)
    {
    }
}