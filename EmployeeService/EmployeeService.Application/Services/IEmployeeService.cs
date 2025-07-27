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
    /// <param name="employee"><see cref="EmployeeRequestDto"/>.</param>
    /// <returns>Индентификатор сотрудника.</returns>
    public Task<int> AddEmployee(EmployeeRequestDto employee);

    /// <summary>
    /// Получить сотрудника.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="EmployeeResponseDto"/>>.</returns>
    public Task<EmployeeResponseDto> GetEmployee(int id, CancellationToken ct = default);

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
    /// <param name="employee"><see cref="EmployeeRequestDto"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task UpdateEmployee(int id, EmployeeRequestDto employee);

    /// <summary>
    /// Получить всех сотрудников.
    /// </summary>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="List{T}"/> of <see cref="EmployeeResponseDto"/></returns>
    public Task<List<EmployeeResponseDto>> GetAllEmployees(CancellationToken ct = default);

    /// <summary>
    /// Получить сотрудников для указанной компании.
    /// </summary>
    /// <param name="companyId">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="List{T}"/> of <see cref="EmployeeResponseDto"/></returns>
    public Task<List<EmployeeResponseDto>> GetEmployeesByCompanyId(int companyId, CancellationToken ct = default);

    /// <summary>
    /// Получить сотрудников для указанного отдела компании.
    /// </summary>
    /// <param name="departmentId">Идентификатор отдела компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="List{T}"/> of <see cref="EmployeeResponseDto"/></returns>
    public Task<List<EmployeeResponseDto>> GetEmployeesByDepartmentId(int departmentId, CancellationToken ct = default);
}