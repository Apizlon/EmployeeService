using EmployeeService.Application.Contracts.Company;
using EmployeeService.Application.Interfaces.Repositories;
using EmployeeService.Application.Interfaces.Services;
using EmployeeService.Application.Mappers;
using EmployeeService.Application.Validators;
using EmployeeService.Domain.Exceptions;

namespace EmployeeService.Application.Services;

/// <inheritdoc/>
public class CompanyService : ICompanyService
{
    /// <summary><see cref="ICompanyRepository"/>.</summary>
    private readonly ICompanyRepository _companyRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="companyRepository"><see cref="ICompanyRepository"/>..</param>
    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    /// <inheritdoc/>
    public async Task<int> AddCompanyAsync(CompanyRequest company)
    {
        company.Validate();
        var id = await _companyRepository.AddCompanyAsync(company.MapToDomain());
        return id;
    }

    /// <inheritdoc/>
    public async Task<CompanyResponse?> GetCompanyAsync(int id, CancellationToken ct = default)
    {
        var company = await _companyRepository.GetCompanyAsync(id, ct) ?? throw new CompanyNotFoundException(id);
        return company.MapToDto();
    }

    /// <inheritdoc/>
    public async Task DeleteCompanyAsync(int id)
    {
        var exist = await _companyRepository.CompanyExistsAsync(id);
        if (!exist)
        {
            throw new CompanyNotFoundException(id);
        }

        await _companyRepository.DeleteCompanyAsync(id);
    }

    /// <inheritdoc/>
    public async Task UpdateCompanyAsync(int id, CompanyRequest company)
    {
        var exist = await _companyRepository.CompanyExistsAsync(id);
        if (!exist)
        {
            throw new CompanyNotFoundException(id);
        }

        company.Validate();
        await _companyRepository.UpdateCompanyAsync(company.MapToDomain(id));
    }
}