namespace EmployeeService.Application.Interfaces.UnitOfWork;

/// <summary>
/// Действие при вызове Dispose.
/// </summary>
public enum OnDispose
{
    /// <summary>
    /// Откат транзакции.
    /// </summary>
    Rollback,
    /// <summary>
    /// Подтверждение транзакции.
    /// </summary>
    Commit
}