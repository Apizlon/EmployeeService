using EmployeeService.Application.Contracts;
using EmployeeService.Application.Contracts.Company;
using EmployeeService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly ILogger<CompanyController> _logger;

    public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
    {
        _companyService = companyService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<int> AddCompanyAsync(CompanyRequest company)
    {
        _logger.LogInformation("Adding a company.");
        var id = await _companyService.AddCompany(company);
        _logger.LogInformation($"Company with id {id} added.");
        return id;
    }
    
    [HttpGet("{id:int}")]
    public async Task<CompanyResponse> GetCompanyAsync(int id, CancellationToken ct = default)
    {
        _logger.LogInformation($"Obtaining a company with id {id}");
        var company = await _companyService.GetCompany(id, ct);
        _logger.LogInformation($"Company with id {id} successfully obtained.");
        return company!;
    }
    
    [HttpPatch("{id:int}")]
    public async Task UpdateCompanyAsync(int id, CompanyRequest company)
    {
        _logger.LogInformation($"Updating a company with id {id}");
        await _companyService.UpdateCompany(id, company);
        _logger.LogInformation($"Company with id {id} successfully updated.");
    }
    
    [HttpDelete("{id:int}")]
    public async Task DeleteCompanyAsync(int id)
    {
        _logger.LogInformation($"Deleting a company with id {id}");
        await _companyService.DeleteCompany(id);
        _logger.LogInformation($"Company with id {id} successfully deleted.");
    }
}