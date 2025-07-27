using System.Data;

namespace EmployeeService.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий, поддерживающий транзакции.
/// </summary>
public interface ITransactionalRepository
{
    /// <summary>
    /// Установить транзацию.
    /// </summary>
    /// <param name="transaction"><see cref="IDepartmentRepository"/>.</param>
    void SetTransaction(IDbTransaction transaction);
}