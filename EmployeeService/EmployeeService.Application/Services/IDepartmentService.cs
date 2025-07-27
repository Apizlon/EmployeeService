using EmployeeService.Application.Contracts.Department;

namespace EmployeeService.Application.Services;

/// <summary>
/// Сервис для работы с отделами.
/// </summary>
public interface IDepartmentService
{
    /// <summary>
    /// Добавить компанию.
    /// </summary>
    /// <param name="department"><see cref="AddDepartmentRequest"/>.</param>
    /// <returns>Индентификатор компании.</returns>
    public Task<int> AddDepartmentAsync(AddDepartmentRequest department);

    /// <summary>
    /// Получить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="DepartmentResponse"/>>.</returns>
    public Task<DepartmentResponse?> GetDepartmentAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Удалить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <returns><see cref="Task"/></returns>
    public Task DeleteDepartmentAsync(int id);

    /// <summary>
    /// Обновить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="department"><see cref="UpdateDepartmentRequest"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task UpdateDepartmentAsync(int id, UpdateDepartmentRequest department);
}