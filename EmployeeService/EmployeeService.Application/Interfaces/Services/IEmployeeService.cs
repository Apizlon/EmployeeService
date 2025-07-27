using EmployeeService.Application.Contracts.Employee;

namespace EmployeeService.Application.Interfaces.Services;

/// <summary>
/// Сервис для работы с сотрудниками.
/// </summary>
public interface IEmployeeService
{
    /// <summary>
    /// Добавить сотрудника.
    /// </summary>
    /// <param name="employee"><see cref="AddEmployeeRequest"/>.</param>
    /// <returns>Индентификатор сотрудника.</returns>
    public Task<int> AddEmployeeAsync(AddEmployeeRequest employee);

    /// <summary>
    /// Получить сотрудника.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="EmployeeResponse"/>>.</returns>
    public Task<EmployeeResponse> GetEmployeeAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Удалить сотрудника.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <returns><see cref="Task"/></returns>
    public Task DeleteEmployeeAsync(int id);

    /// <summary>
    /// Обновить сотрудника.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="employee"><see cref="UpdateEmployeeRequest"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task UpdateEmployeeAsync(int id, UpdateEmployeeRequest employee);

    /// <summary>
    /// Получить всех сотрудников.
    /// </summary>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="List{T}"/> of <see cref="EmployeeResponse"/></returns>
    public Task<List<EmployeeResponse>> GetAllEmployeesAsync(CancellationToken ct = default);

    /// <summary>
    /// Получить сотрудников для указанной компании.
    /// </summary>
    /// <param name="companyId">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="List{T}"/> of <see cref="EmployeeResponse"/></returns>
    public Task<List<EmployeeResponse>> GetEmployeesByCompanyIdAsync(int companyId, CancellationToken ct = default);

    /// <summary>
    /// Получить сотрудников для указанного отдела компании.
    /// </summary>
    /// <param name="departmentId">Идентификатор отдела компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="List{T}"/> of <see cref="EmployeeResponse"/></returns>
    public Task<List<EmployeeResponse>> GetEmployeesByDepartmentIdAsync(int departmentId, CancellationToken ct = default);
}