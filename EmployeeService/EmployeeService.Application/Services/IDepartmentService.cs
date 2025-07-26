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
    public Task<int> AddDepartment(AddDepartmentRequest department);

    /// <summary>
    /// Получить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="DepartmentResponse"/>>.</returns>
    public Task<DepartmentResponse?> GetDepartment(int id, CancellationToken ct = default);

    /// <summary>
    /// Существует ли компания.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns>true если существует, иначе - false</returns>
    public Task<bool> DepartmentExists(int id, CancellationToken ct = default);

    /// <summary>
    /// Удалить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <returns><see cref="Task"/></returns>
    public Task DeleteDepartment(int id);

    /// <summary>
    /// Обновить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="department)"><see cref="UpdateDepartmentRequest"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task UpdateDepartment(int id, UpdateDepartmentRequest department);
}