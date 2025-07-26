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
    public async Task<int> AddDepartment(AddDepartmentRequest department)
    {
        var companyExists = await _companyRepository.CompanyExists(department.CompanyId);
        department.ValidateAdd(companyExists);
        var id = await _departmentRepository.AddDepartment(department.MapToDomain());
        return id;
    }

    /// <inheritdoc/>
    public async Task<DepartmentResponse?> GetDepartment(int id, CancellationToken ct = default)
    {
        var department = await _departmentRepository.GetDepartment(id, ct) ?? throw new DepartmentNotFoundException(id);
        return department.MapToDto();
    }

    /// <inheritdoc/>
    public async Task<bool> DepartmentExists(int id, CancellationToken ct = default)
    {
        var exists = await _departmentRepository.DepartmentExists(id, ct);
        return exists;
    }

    /// <inheritdoc/>
    public async Task DeleteDepartment(int id)
    {
        var exist = await DepartmentExists(id);
        if (!exist) throw new DepartmentNotFoundException(id);
        await _departmentRepository.DeleteDepartment(id);
    }

    /// <inheritdoc/>
    public async Task UpdateDepartment(int id, UpdateDepartmentRequest department)
    {
        var exist = await DepartmentExists(id);
        if (!exist) throw new DepartmentNotFoundException(id);
        var companyExists = true;
        if (department.CompanyId is not null)
            companyExists = await _companyRepository.CompanyExists(department.CompanyId.Value);
        department.ValidateUpdate(companyExists);
        await _departmentRepository.UpdateDepartment(department.MapToDomain(id));
    }
}