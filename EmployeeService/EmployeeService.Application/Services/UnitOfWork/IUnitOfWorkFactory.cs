namespace EmployeeService.Application.Services.UnitOfWork;

/// <summary>
/// Фабрика UnitofWork.
/// </summary>
public interface IUnitOfWorkFactory
{
    /// <summary>
    /// Создать UniOfWork.
    /// </summary>
    /// <param name="onDispose"><see cref="OnDispose"/>.</param>
    /// <returns><see cref="IUnitOfWork"/>.</returns>
    IUnitOfWork Create(OnDispose onDispose = OnDispose.Commit);
}