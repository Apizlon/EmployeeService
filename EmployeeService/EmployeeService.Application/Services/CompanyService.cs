using EmployeeService.Application.Contracts;
using EmployeeService.Application.Contracts.Company;
using EmployeeService.Application.Mappers;
using EmployeeService.Application.Services.UnitOfWork;
using EmployeeService.Application.Validators;
using EmployeeService.DataAccess.Repositories;
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
    public async Task<int> AddCompany(CompanyRequest company)
    {
        company.Validate();
        var id = await _companyRepository.AddCompany(company.MapToDomain());
        return id;
    }

    /// <inheritdoc/>
    public async Task<CompanyResponse?> GetCompany(int id, CancellationToken ct = default)
    {
        var company = await _companyRepository.GetCompany(id, ct) ?? throw new CompanyNotFoundException(id);
        return company.MapToDto();
    }

    /// <inheritdoc/>
    public async Task<bool> CompanyExists(int id, CancellationToken ct = default)
    {
        var exists = await _companyRepository.CompanyExists(id, ct);
        return exists;
    }

    /// <inheritdoc/>
    public async Task DeleteCompany(int id)
    {
        var exist = await CompanyExists(id);
        if (!exist) throw new CompanyNotFoundException(id);
        await _companyRepository.DeleteCompany(id);
    }

    /// <inheritdoc/>
    public async Task UpdateCompany(int id, CompanyRequest company)
    {
        var exist = await CompanyExists(id);
        if (!exist) throw new CompanyNotFoundException(id);
        company.Validate();
        await _companyRepository.UpdateCompany(company.MapToDomain(id));
    }
}