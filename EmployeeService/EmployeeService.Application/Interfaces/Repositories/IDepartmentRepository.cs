using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий отдела.
/// </summary>
public interface IDepartmentRepository : ITransactionalRepository
{
    /// <summary>
    /// Добавить отдел.
    /// </summary>
    /// <param name="department"><see cref="Department"/>.</param>
    /// <returns>Индентификатор отдела.</returns>
    public Task<int> AddDepartmentAsync(Department department);
    
    /// <summary>
    /// Получить отдел.
    /// </summary>
    /// <param name="id">Идентификатор отдела.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="Department"/>>.</returns>
    public Task<Department?> GetDepartmentAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Существует ли отдел.
    /// </summary>
    /// <param name="id">Идентификатор отдела.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns>true если существует, иначе - false</returns>
    public Task<bool> DepartmentExistsAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Удалить отдел.
    /// </summary>
    /// <param name="id">Идентификатор отдела.</param>
    /// <returns><see cref="Task"/></returns>
    public Task DeleteDepartmentAsync(int id);
    
    /// <summary>
    /// Обновить отдел.
    /// </summary>
    /// <param name="department"><see cref="Department"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task UpdateDepartmentAsync(Department department);
}