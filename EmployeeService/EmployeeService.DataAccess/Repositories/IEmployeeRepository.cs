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
    public Task<int> AddEmployee(Employee employee);

    /// <summary>
    /// Получить сотрудника.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="Employee"/>>.</returns>
    public Task<Employee?> GetEmployee(int id, CancellationToken ct = default);

    /// <summary>
    /// Существует ли сотрудник.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns>true если существует, иначе - false</returns>
    public Task<bool> EmployeeExists(int id, CancellationToken ct = default);

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
    /// <param name="employee"><see cref="Employee"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task UpdateEmployee(int id, Employee employee);

    /// <summary>
    /// Получить всех сотрудников.
    /// </summary>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="Employee"/></returns>
    public Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct = default);

    /// <summary>
    /// Получить сотрудников для указанной компании.
    /// </summary>
    /// <param name="companyId">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="Employee"/></returns>
    public Task<IEnumerable<Employee>> GetEmployeesByCompanyId(int companyId, CancellationToken ct = default);

    /// <summary>
    /// Получить сотрудников для указанного отдела компании.
    /// </summary>
    /// <param name="departmentId">Идентификатор отдела компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="Employee"/></returns>
    public Task<IEnumerable<Employee>> GetEmployeesByDepartmentId(int departmentId, CancellationToken ct = default);
}