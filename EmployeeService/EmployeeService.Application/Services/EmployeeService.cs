using System.Runtime.CompilerServices;
using EmployeeService.Application.Contracts.Employee;
using EmployeeService.Application.Interfaces.Repositories;
using EmployeeService.Application.Interfaces.Services;
using EmployeeService.Application.Interfaces.UnitOfWork;
using EmployeeService.Application.Mappers;
using EmployeeService.Application.Validators;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Exceptions;

namespace EmployeeService.Application.Services;

/// <inheritdoc/>
public class EmployeeService : IEmployeeService
{
    /// <summary><see cref="IUnitOfWorkFactory"/>.</summary>
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="unitOfWorkFactory"><see cref="IUnitOfWorkFactory"/>.</param>
    public EmployeeService(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    /// <inheritdoc/>
    public async Task<int> AddEmployeeAsync(AddEmployeeRequest employee)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        unitOfWork.BeginTransaction();
        var departmentRepository = unitOfWork.GetRepository<IDepartmentRepository>();
        var employeeRepository = unitOfWork.GetRepository<IEmployeeRepository>();
        var passportRepository = unitOfWork.GetRepository<IPassportRepository>();

        var department = await departmentRepository.GetDepartmentAsync(employee.DepartmentId) ??
                         throw new DepartmentNotFoundException(employee.DepartmentId);
        employee.ValidateAdd(department);

        var passportId = await passportRepository.AddPassportAsync(employee.Passport!.MapToDomain());
        var employeeId = await employeeRepository.AddEmployeeAsync(employee.MapToDomain(passportId));
        unitOfWork.Commit();

        return employeeId;
    }

    /// <inheritdoc/>
    public async Task<EmployeeResponse> GetEmployeeAsync(int id, CancellationToken ct = default)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        var employeeRepository = unitOfWork.GetRepository<IEmployeeRepository>();
        var passportRepository = unitOfWork.GetRepository<IPassportRepository>();
        var departmentRepository = unitOfWork.GetRepository<IDepartmentRepository>();

        var employee = await employeeRepository.GetEmployeeAsync(id, ct) ?? throw new EmployeeNotFoundException(id);
        var passport = await passportRepository.GetPassportAsync(employee.PassportId, ct) ??
                       throw new PassportNotFoundException(employee.PassportId);
        var department = await departmentRepository.GetDepartmentAsync(employee.DepartmentId, ct) ??
                         throw new DepartmentNotFoundException(employee.DepartmentId);

        var result = employee.MapToDto(passport, department);
        return result;
    }

    /// <inheritdoc/>
    public async Task DeleteEmployeeAsync(int id)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        unitOfWork.BeginTransaction();

        var employeeRepository = unitOfWork.GetRepository<IEmployeeRepository>();
        var oldEmployee = await employeeRepository.GetEmployeeAsync(id) ?? throw new EmployeeNotFoundException(id);
        var passportRepository = unitOfWork.GetRepository<IPassportRepository>();

        await employeeRepository.DeleteEmployeeAsync(id);
        await passportRepository.DeletePassportAsync(oldEmployee.PassportId);

        unitOfWork.Commit();
    }

    /// <inheritdoc/>
    public async Task UpdateEmployeeAsync(int id, UpdateEmployeeRequest employee)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        unitOfWork.BeginTransaction();

        var employeeRepository = unitOfWork.GetRepository<IEmployeeRepository>();
        var oldEmployee = await employeeRepository.GetEmployeeAsync(id) ?? throw new EmployeeNotFoundException(id);
        var departmentRepository = unitOfWork.GetRepository<IDepartmentRepository>();

        await employee.ValidateUpdate(oldEmployee, departmentRepository);
        await employeeRepository.UpdateEmployeeAsync(id, employee.MapToDomain());

        if (employee.Passport is not null)
        {
            employee.Passport.ValidateUpdate();
            var passportRepository = unitOfWork.GetRepository<IPassportRepository>();
            await passportRepository.UpdatePassportAsync(employee.Passport.MapToDomain(oldEmployee.PassportId));
        }

        unitOfWork.Commit();
    }

    /// <inheritdoc/>
    public async Task<List<EmployeeResponse>> GetAllEmployeesAsync(CancellationToken ct = default)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        var employeeRepository = unitOfWork.GetRepository<IEmployeeRepository>();
        var passportRepository = unitOfWork.GetRepository<IPassportRepository>();
        var departmentRepository = unitOfWork.GetRepository<IDepartmentRepository>();

        var employees = await employeeRepository.GetAllEmployeesAsync(ct);
        var result = ObtainPassportAndDepartmentForEmployees(passportRepository, departmentRepository, employees, ct);

        return await result.ToListAsync(ct);
    }

    /// <inheritdoc/>
    public async Task<List<EmployeeResponse>> GetEmployeesByCompanyIdAsync(int companyId,
        CancellationToken ct = default)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        var employeeRepository = unitOfWork.GetRepository<IEmployeeRepository>();
        var passportRepository = unitOfWork.GetRepository<IPassportRepository>();
        var departmentRepository = unitOfWork.GetRepository<IDepartmentRepository>();

        var employees = await employeeRepository.GetEmployeesByCompanyIdAsync(companyId, ct);
        var result = ObtainPassportAndDepartmentForEmployees(passportRepository, departmentRepository, employees, ct);

        return await result.ToListAsync(ct);
    }

    /// <inheritdoc/>
    public async Task<List<EmployeeResponse>> GetEmployeesByDepartmentIdAsync(int departmentId,
        CancellationToken ct = default)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        var employeeRepository = unitOfWork.GetRepository<IEmployeeRepository>();
        var passportRepository = unitOfWork.GetRepository<IPassportRepository>();
        var departmentRepository = unitOfWork.GetRepository<IDepartmentRepository>();

        var employees = await employeeRepository.GetEmployeesByDepartmentIdAsync(departmentId, ct);
        var result = ObtainPassportAndDepartmentForEmployees(passportRepository, departmentRepository, employees, ct);

        return await result.ToListAsync(ct);
    }

    private static async IAsyncEnumerable<EmployeeResponse> ObtainPassportAndDepartmentForEmployees(
        IPassportRepository passportRepository, IDepartmentRepository departmentRepository,
        IEnumerable<Employee> employees, [EnumeratorCancellation] CancellationToken ct = default)
    {
        foreach (var employee in employees)
        {
            var passport = await passportRepository.GetPassportAsync(employee.PassportId, ct) ??
                           throw new PassportNotFoundException(employee.PassportId);
            var department = await departmentRepository.GetDepartmentAsync(employee.DepartmentId, ct) ??
                             throw new DepartmentNotFoundException(employee.DepartmentId);
            yield return employee.MapToDto(passport, department);
        }
    }
}