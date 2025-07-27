using EmployeeService.Domain.Entities;

namespace EmployeeService.DataAccess.Repositories;

/// <summary>
/// Репозиторий сотрудников.
/// </summary>
public interface IEmployeeRepository : ITransactionalRepository
{
    /// <summary>
    /// Добавить сотрудника.
    /// </summary>
    /// <param name="employee"><see cref="Employee"/>.</param>
    /// <returns>Индентификатор сотрудника.</returns>
    public Task<int> AddEmployeeAsync(Employee employee);

    /// <summary>
    /// Получить сотрудника.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="Employee"/>>.</returns>
    public Task<Employee?> GetEmployeeAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Существует ли сотрудник.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns>true если существует, иначе - false</returns>
    public Task<bool> EmployeeExistsAsync(int id, CancellationToken ct = default);

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
    /// <param name="employee"><see cref="Employee"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task UpdateEmployeeAsync(int id, Employee employee);

    /// <summary>
    /// Получить всех сотрудников.
    /// </summary>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="Employee"/></returns>
    public Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken ct = default);

    /// <summary>
    /// Получить сотрудников для указанной компании.
    /// </summary>
    /// <param name="companyId">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="Employee"/></returns>
    public Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(int companyId, CancellationToken ct = default);

    /// <summary>
    /// Получить сотрудников для указанного отдела компании.
    /// </summary>
    /// <param name="departmentId">Идентификатор отдела компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="Employee"/></returns>
    public Task<IEnumerable<Employee>> GetEmployeesByDepartmentIdAsync(int departmentId, CancellationToken ct = default);
}