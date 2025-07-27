using EmployeeService.Application.Contracts.Department;
using EmployeeService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers;

/// <summary>
/// API для управления отделами компании.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    /// <summary><see cref="IDepartmentService"/>.</summary>
    private readonly IDepartmentService _departmentService;
    
    /// <summary><see cref="ILogger{TCategoryName}"/>.</summary>
    private readonly ILogger<DepartmentController> _logger;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="departmentService"><see cref="IDepartmentService"/>.></param>
    /// <param name="logger"><see cref="ILogger{TCategoryName}"/>.</param>
    public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
    {
        _departmentService = departmentService;
        _logger = logger;
    }

    /// <summary>
    /// Добавить отдел компании.
    /// </summary>
    /// <param name="addDepartment"><see cref="AddDepartmentRequest"/></param>
    /// <returns>Идентификатор отдела компании.</returns>
    [HttpPost]
    public async Task<int> AddDepartmentAsync(AddDepartmentRequest addDepartment)
    {
        _logger.LogInformation("Adding a department.");
        var id = await _departmentService.AddDepartmentAsync(addDepartment);
        _logger.LogInformation($"Department with id {id} added.");
        return id;
    }

    /// <summary>
    /// Получить отдел компании.
    /// </summary>
    /// <param name="id">Идентификатор отдела компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="DepartmentResponse"/>.</returns>
    [HttpGet("{id:int}")]
    public async Task<DepartmentResponse> GetDepartmentAsync(int id, CancellationToken ct = default)
    {
        _logger.LogInformation($"Obtaining a department with id {id}");
        var department = await _departmentService.GetDepartmentAsync(id, ct);
        _logger.LogInformation($"Department with id {id} successfully obtained.");
        return department!;
    }

    /// <summary>
    /// Обновить отдел компании.
    /// </summary>
    /// <param name="id">Идентификатор отдела компании.</param>
    /// <param name="updateDepartmentRequest"><see cref="UpdateDepartmentRequest"/>.</param>
    [HttpPatch("{id:int}")]
    public async Task UpdateDepartmentAsync(int id, UpdateDepartmentRequest updateDepartmentRequest)
    {
        _logger.LogInformation($"Updating a department with id {id}");
        await _departmentService.UpdateDepartmentAsync(id, updateDepartmentRequest);
        _logger.LogInformation($"Department with id {id} successfully updated.");
    }

    /// <summary>
    /// Удалить отдел компании.
    /// </summary>
    /// <param name="id">Идентификатор отдела компании.</param>
    [HttpDelete("{id:int}")]
    public async Task DeleteDepartmentAsync(int id)
    {
        _logger.LogInformation($"Deleting a department with id {id}");
        await _departmentService.DeleteDepartmentAsync(id);
        _logger.LogInformation($"Department with id {id} successfully deleted.");
    }
}