using EmployeeService.Application.Contracts.Employee;
using EmployeeService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers;

/// <summary>
/// API для управления сотрудниками.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    /// <summary><see cref="IEmployeeService"/>.</summary>
    private readonly IEmployeeService _employeeService;

    /// <summary><see cref="ILogger{TCategoryName}"/>.</summary>
    private readonly ILogger<EmployeeController> _logger;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="employeeService"><see cref="IEmployeeService"/>.></param>
    /// <param name="logger"><see cref="ILogger{TCategoryName}"/>.</param>
    public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    /// <summary>
    /// Добавить сотрудника.
    /// </summary>
    /// <param name="employee"><see cref="AddEmployeeRequest"/>.</param>
    /// <returns>Идентификатор сотрудника.</returns>
    [HttpPost]
    public async Task<int> AddEmployeeAsync(AddEmployeeRequest employee)
    {
        _logger.LogInformation("Adding a employee.");
        var id = await _employeeService.AddEmployeeAsync(employee);
        _logger.LogInformation($"Employee with id {id} added.");
        return id;
    }

    /// <summary>
    /// Получить сотрудника по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="EmployeeResponse"/>.</returns>
    [HttpGet("{id:int}")]
    public async Task<EmployeeResponse> GetEmployeeAsync(int id, CancellationToken ct = default)
    {
        _logger.LogInformation($"Obtaining a employee with id {id}.");
        var employee = await _employeeService.GetEmployeeAsync(id, ct);
        _logger.LogInformation($"Employee with id {id} successfully obtained.");
        return employee!;
    }

    /// <summary>
    /// Обновить сотрудника по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="employee"><see cref="UpdateEmployeeRequest"/>.</param>
    [HttpPatch("{id:int}")]
    public async Task UpdateEmployeeAsync(int id, UpdateEmployeeRequest employee)
    {
        _logger.LogInformation($"Updating a employee with id {id}.");
        await _employeeService.UpdateEmployeeAsync(id, employee);
        _logger.LogInformation($"Employee with id {id} successfully updated.");
    }

    /// <summary>
    /// Удалить сотрудника по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    [HttpDelete("{id:int}")]
    public async Task DeleteEmployeeAsync(int id)
    {
        _logger.LogInformation($"Deleting a employee with id {id}.");
        await _employeeService.DeleteEmployeeAsync(id);
        _logger.LogInformation($"Employee with id {id} successfully deleted.");
    }

    /// <summary>
    /// Получить всех сотрудников.
    /// </summary>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="List{T}"/> of <see cref="EmployeeResponse"/>.</returns>
    [HttpGet]
    public async Task<List<EmployeeResponse>> GetAllEmployeesAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Obtaining all employees.");
        var employees = await _employeeService.GetAllEmployeesAsync(ct);
        _logger.LogInformation("All employees successfully obtained.");
        return employees;
    }

    /// <summary>
    /// Получить сотрудников для указанной компании.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="List{T}"/> of <see cref="EmployeeResponse"/>.</returns>
    [HttpGet("company/{id:int}")]
    public async Task<List<EmployeeResponse>> GetEmployeeByCompanyIdAsync(int id, CancellationToken ct = default)
    {
        _logger.LogInformation($"Obtaining employees from company with id {id}.");
        var employees = await _employeeService.GetEmployeesByCompanyIdAsync(id, ct);
        _logger.LogInformation($"Employees from company with id {id} successfully obtained.");
        return employees;
    }

    /// <summary>
    /// Получить сотрудников для указанного отдела.
    /// </summary>
    /// <param name="id">Идентификатор отдела.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="List{T}"/> of <see cref="EmployeeResponse"/>.</returns>
    [HttpGet("department/{id:int}")]
    public async Task<List<EmployeeResponse>> GetEmployeeByDepartmentIdAsync(int id, CancellationToken ct = default)
    {
        _logger.LogInformation($"Obtaining employees from department with id {id}.");
        var employees = await _employeeService.GetEmployeesByDepartmentIdAsync(id, ct);
        _logger.LogInformation($"Employees from department with id {id} successfully obtained.");
        return employees;
    }
}