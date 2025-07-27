using EmployeeService.Application.Contracts.Employee;
using EmployeeService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<int> AddEmployeeAsync(EmployeeRequestDto employee)
    {
        _logger.LogInformation("Adding a employee.");
        var id = await _employeeService.AddEmployee(employee);
        _logger.LogInformation($"Employee with id {id} added.");
        return id;
    }
    
    [HttpGet("{id:int}")]
    public async Task<EmployeeResponseDto> GetEmployeeAsync(int id, CancellationToken ct = default)
    {
        _logger.LogInformation($"Obtaining a employee with id {id}.");
        var employee = await _employeeService.GetEmployee(id, ct);
        _logger.LogInformation($"Employee with id {id} successfully obtained.");
        return employee!;
    }
    
    [HttpPatch("{id:int}")]
    public async Task UpdateEmployeeAsync(int id, EmployeeRequestDto employee)
    {
        _logger.LogInformation($"Updating a employee with id {id}.");
        await _employeeService.UpdateEmployee(id, employee);
        _logger.LogInformation($"Employee with id {id} successfully updated.");
    }
    
    [HttpDelete("{id:int}")]
    public async Task DeleteEmployeeAsync(int id)
    {
        _logger.LogInformation($"Deleting a employee with id {id}.");
        await _employeeService.DeleteEmployee(id);
        _logger.LogInformation($"Employee with id {id} successfully deleted.");
    }
    
    [HttpGet]
    public async Task<List<EmployeeResponseDto>> GetAllEmployeesAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Obtaining all employees.");
        var employees = await _employeeService.GetAllEmployees(ct);
        _logger.LogInformation("All employees successfully obtained.");
        return employees;
    }
    
    [HttpGet("company/{id:int}")]
    public async Task<List<EmployeeResponseDto>> GetEmployeeByCompanyIdAsync(int id, CancellationToken ct = default)
    {
        _logger.LogInformation($"Obtaining employees from company with id {id}.");
        var employees = await _employeeService.GetEmployeesByCompanyId(id, ct);
        _logger.LogInformation($"Employees from company with id {id} successfully obtained.");
        return employees;
    }
    
    [HttpGet("department/{id:int}")]
    public async Task<List<EmployeeResponseDto>> GetEmployeeByDepartmentIdAsync(int id, CancellationToken ct = default)
    {
        _logger.LogInformation($"Obtaining employees from department with id {id}.");
        var employees = await _employeeService.GetEmployeesByDepartmentId(id, ct);
        _logger.LogInformation($"Employees from department with id {id} successfully obtained.");
        return employees;
    }
}