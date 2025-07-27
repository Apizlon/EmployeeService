using EmployeeService.Application.Contracts.Employee;

namespace EmployeeService.Application.Services;

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
    public Task<int> AddEmployee(AddEmployeeRequest employee);

    /// <summary>
    /// Получить сотрудника.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="EmployeeResponse"/>>.</returns>
    public Task<EmployeeResponse> GetEmployee(int id, CancellationToken ct = default);

    /// <summary>
    /// Удалить сотрудника.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <returns><see cref="Task"/></returns>
    public Task DeleteEmployee(int id);

    /// <summary>
    /// Обновить сотрудника.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="employee"><see cref="UpdateEmployeeRequest"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task UpdateEmployee(int id, UpdateEmployeeRequest employee);

    /// <summary>
    /// Получить всех сотрудников.
    /// </summary>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="List{T}"/> of <see cref="EmployeeResponse"/></returns>
    public Task<List<EmployeeResponse>> GetAllEmployees(CancellationToken ct = default);

    /// <summary>
    /// Получить сотрудников для указанной компании.
    /// </summary>
    /// <param name="companyId">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="List{T}"/> of <see cref="EmployeeResponse"/></returns>
    public Task<List<EmployeeResponse>> GetEmployeesByCompanyId(int companyId, CancellationToken ct = default);

    /// <summary>
    /// Получить сотрудников для указанного отдела компании.
    /// </summary>
    /// <param name="departmentId">Идентификатор отдела компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="List{T}"/> of <see cref="EmployeeResponse"/></returns>
    public Task<List<EmployeeResponse>> GetEmployeesByDepartmentId(int departmentId, CancellationToken ct = default);
}