using EmployeeService.Application.Contracts.Department;
using EmployeeService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;
    private readonly ILogger<DepartmentController> _logger;

    public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
    {
        _departmentService = departmentService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<int> AddDepartmentAsync(AddDepartmentRequest addDepartment)
    {
        _logger.LogInformation("Adding a department.");
        var id = await _departmentService.AddDepartment(addDepartment);
        _logger.LogInformation($"Department with id {id} added.");
        return id;
    }

    [HttpGet("{id:int}")]
    public async Task<DepartmentResponse> GetDepartmentAsync(int id, CancellationToken ct = default)
    {
        _logger.LogInformation($"Obtaining a department with id {id}");
        var department = await _departmentService.GetDepartment(id, ct);
        _logger.LogInformation($"Department with id {id} successfully obtained.");
        return department!;
    }

    [HttpPatch("{id:int}")]
    public async Task UpdateDepartmentAsync(int id, UpdateDepartmentRequest updateDepartmentRequest)
    {
        _logger.LogInformation($"Updating a department with id {id}");
        await _departmentService.UpdateDepartment(id, updateDepartmentRequest);
        _logger.LogInformation($"Department with id {id} successfully updated.");
    }

    [HttpDelete("{id:int}")]
    public async Task DeleteDepartmentAsync(int id)
    {
        _logger.LogInformation($"Deleting a department with id {id}");
        await _departmentService.DeleteDepartment(id);
        _logger.LogInformation($"Department with id {id} successfully deleted.");
    }
}