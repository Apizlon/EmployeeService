namespace EmployeeService.Application.Services.UnitOfWork;

/// <summary>
/// Unit of Work.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Начать транзакцию.
    /// </summary>
    void BeginTransaction();
    
    /// <summary>
    /// Закоммитить транзакцию.
    /// </summary>
    void Commit();
    
    /// <summary>
    /// Откатить транзацию.
    /// </summary>
    void Rollback();
    
    /// <summary>
    /// Получить репозиторий.
    /// </summary>
    /// <typeparam name="T">Тип репозитория.</typeparam>
    /// <returns>Репозиторий.</returns>
    T GetRepository<T>() where T : class;
} 