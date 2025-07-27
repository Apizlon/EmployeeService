using EmployeeService.Application.Contracts.Department;
using EmployeeService.Application.Mappers;
using EmployeeService.Application.Validators;
using EmployeeService.DataAccess.Repositories;
using EmployeeService.Domain.Exceptions;

namespace EmployeeService.Application.Services;

/// <inheritdoc/>
public class DepartmentService : IDepartmentService
{
    /// <summary><see cref="IDepartmentRepository"/>.</summary>
    private readonly IDepartmentRepository _departmentRepository;

    /// <summary><see cref="ICompanyRepository"/>.</summary>
    private readonly ICompanyRepository _companyRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="departmentRepository"><see cref="IDepartmentRepository"/>.</param>\
    /// <param name="companyRepository"><see cref="ICompanyRepository"/>..</param>
    public DepartmentService(IDepartmentRepository departmentRepository, ICompanyRepository companyRepository)
    {
        _departmentRepository = departmentRepository;
        _companyRepository = companyRepository;
    }

    /// <inheritdoc/>
    public async Task<int> AddDepartmentAsync(AddDepartmentRequest department)
    {
        var companyExists = await _companyRepository.CompanyExistsAsync(department.CompanyId);
        department.ValidateAdd(companyExists);
        var id = await _departmentRepository.AddDepartmentAsync(department.MapToDomain());
        return id;
    }

    /// <inheritdoc/>
    public async Task<DepartmentResponse?> GetDepartmentAsync(int id, CancellationToken ct = default)
    {
        var department = await _departmentRepository.GetDepartmentAsync(id, ct) ??
                         throw new DepartmentNotFoundException(id);
        return department.MapToDto();
    }

    /// <inheritdoc/>
    public async Task DeleteDepartmentAsync(int id)
    {
        if (!await _departmentRepository.DepartmentExistsAsync(id))
        {
            throw new DepartmentNotFoundException(id);
        }

        await _departmentRepository.DeleteDepartmentAsync(id);
    }

    /// <inheritdoc/>
    public async Task UpdateDepartmentAsync(int id, UpdateDepartmentRequest department)
    {
        if (!await _departmentRepository.DepartmentExistsAsync(id))
        {
            throw new DepartmentNotFoundException(id);
        }

        var companyExists = true;
        if (department.CompanyId is not null)
        {
            companyExists = await _companyRepository.CompanyExistsAsync(department.CompanyId.Value);
        }

        department.ValidateUpdate(companyExists);

        await _departmentRepository.UpdateDepartmentAsync(department.MapToDomain(id));
    }
}