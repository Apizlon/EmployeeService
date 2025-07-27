using EmployeeService.Application.Contracts.Company;
using EmployeeService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers;

/// <summary>
/// API для управления компаниями.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    /// <summary><see cref="ICompanyService"/>.</summary>
    private readonly ICompanyService _companyService;
    
    /// <summary><see cref="ILogger{TCategoryName}"/>.</summary>
    private readonly ILogger<CompanyController> _logger;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="companyService"><see cref="ICompanyService"/></param>
    /// <param name="logger"><see cref="ILogger{TCategoryName}"/>.</param>
    public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
    {
        _companyService = companyService;
        _logger = logger;
    }

    /// <summary>
    /// Добавить компанию.
    /// </summary>
    /// <param name="company"><see cref="CompanyRequest"/>.</param>
    /// <returns>Идентификатор компании.</returns>
    [HttpPost]
    public async Task<int> AddCompanyAsync(CompanyRequest company)
    {
        _logger.LogInformation("Adding a company.");
        var id = await _companyService.AddCompanyAsync(company);
        _logger.LogInformation($"Company with id {id} added.");
        return id;
    }
    
    /// <summary>
    /// Получить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="CompanyResponse"/>.</returns>
    [HttpGet("{id:int}")]
    public async Task<CompanyResponse> GetCompanyAsync(int id, CancellationToken ct = default)
    {
        _logger.LogInformation($"Obtaining a company with id {id}");
        var company = await _companyService.GetCompanyAsync(id, ct);
        _logger.LogInformation($"Company with id {id} successfully obtained.");
        return company!;
    }
    
    /// <summary>
    /// Обновить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="company"><see cref="CompanyRequest"/></param>
    [HttpPatch("{id:int}")]
    public async Task UpdateCompanyAsync(int id, CompanyRequest company)
    {
        _logger.LogInformation($"Updating a company with id {id}");
        await _companyService.UpdateCompanyAsync(id, company);
        _logger.LogInformation($"Company with id {id} successfully updated.");
    }
    
    /// <summary>
    /// Удалить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    [HttpDelete("{id:int}")]
    public async Task DeleteCompanyAsync(int id)
    {
        _logger.LogInformation($"Deleting a company with id {id}");
        await _companyService.DeleteCompanyAsync(id);
        _logger.LogInformation($"Company with id {id} successfully deleted.");
    }
}