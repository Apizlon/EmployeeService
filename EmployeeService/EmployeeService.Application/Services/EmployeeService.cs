using System.Runtime.CompilerServices;
using EmployeeService.Application.Contracts.Employee;
using EmployeeService.Application.Mappers;
using EmployeeService.Application.Services.UnitOfWork;
using EmployeeService.Application.Validators;
using EmployeeService.DataAccess.Repositories;
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
    public async Task<int> AddEmployee(EmployeeRequestDto employee)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        unitOfWork.BeginTransaction();
        var departmentRepository = unitOfWork.GetRepository<DepartmentRepository>();
        var employeeRepository = unitOfWork.GetRepository<EmployeeRepository>();
        var passportRepository = unitOfWork.GetRepository<PassportRepository>();

        var department = await departmentRepository.GetDepartment(employee.DepartmentId) ??
                         throw new DepartmentNotFoundException(employee.DepartmentId);
        employee.ValidateAdd(department);

        var passportId = await passportRepository.AddPassport(employee.Passport!.MapToDomain());
        var employeeId = await employeeRepository.AddEmployee(employee.MapToDomain(passportId));
        unitOfWork.Commit();

        return employeeId;
    }

    /// <inheritdoc/>
    public async Task<EmployeeResponseDto> GetEmployee(int id, CancellationToken ct = default)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        var employeeRepository = unitOfWork.GetRepository<EmployeeRepository>();
        var passportRepository = unitOfWork.GetRepository<PassportRepository>();
        var departmentRepository = unitOfWork.GetRepository<DepartmentRepository>();

        var employee = await employeeRepository.GetEmployee(id, ct) ?? throw new EmployeeNotFoundException(id);
        var passport = await passportRepository.GetPassport(employee.PassportId, ct) ??
                       throw new PassportNotFoundException(employee.PassportId);
        var department = await departmentRepository.GetDepartment(employee.DepartmentId, ct) ??
                         throw new DepartmentNotFoundException(employee.DepartmentId);
        var result = employee.MapToDto(passport, department);
        return result;
    }

    /// <inheritdoc/>
    public async Task DeleteEmployee(int id)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        var employeeRepository = unitOfWork.GetRepository<EmployeeRepository>();
        var employeeExists = await employeeRepository.EmployeeExists(id);
        if (!employeeExists) throw new EmployeeNotFoundException(id);
        await employeeRepository.DeleteEmployee(id);
    }

    /// <inheritdoc/>
    public async Task UpdateEmployee(int id, EmployeeRequestDto employee)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        var employeeRepository = unitOfWork.GetRepository<EmployeeRepository>();
        var employeeExists = await employeeRepository.EmployeeExists(id);
        if (!employeeExists) throw new EmployeeNotFoundException(id);
    }

    /// <inheritdoc/>
    public async Task<List<EmployeeResponseDto>> GetAllEmployees(CancellationToken ct = default)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        var employeeRepository = unitOfWork.GetRepository<EmployeeRepository>();
        var passportRepository = unitOfWork.GetRepository<PassportRepository>();
        var departmentRepository = unitOfWork.GetRepository<DepartmentRepository>();

        var employees = await employeeRepository.GetAllEmployees(ct);
        var result = ObtainPassportAndDepartmentForEmployees(passportRepository, departmentRepository, employees, ct);
        return await result.ToListAsync(ct);
    }

    /// <inheritdoc/>
    public async Task<List<EmployeeResponseDto>> GetEmployeesByCompanyId(int companyId, CancellationToken ct = default)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        var employeeRepository = unitOfWork.GetRepository<EmployeeRepository>();
        var passportRepository = unitOfWork.GetRepository<PassportRepository>();
        var departmentRepository = unitOfWork.GetRepository<DepartmentRepository>();

        var employees = await employeeRepository.GetEmployeesByCompanyId(companyId, ct);
        var result = ObtainPassportAndDepartmentForEmployees(passportRepository, departmentRepository, employees, ct);
        return await result.ToListAsync(ct);
    }

    /// <inheritdoc/>
    public async Task<List<EmployeeResponseDto>> GetEmployeesByDepartmentId(int departmentId, CancellationToken ct = default)
    {
        using var unitOfWork = _unitOfWorkFactory.Create();
        var employeeRepository = unitOfWork.GetRepository<EmployeeRepository>();
        var passportRepository = unitOfWork.GetRepository<PassportRepository>();
        var departmentRepository = unitOfWork.GetRepository<DepartmentRepository>();

        var employees = await employeeRepository.GetEmployeesByDepartmentId(departmentId, ct);
        var result = ObtainPassportAndDepartmentForEmployees(passportRepository, departmentRepository, employees, ct);
        return await result.ToListAsync(ct);
    }

    private static async IAsyncEnumerable<EmployeeResponseDto> ObtainPassportAndDepartmentForEmployees(
        PassportRepository passportRepository, DepartmentRepository departmentRepository,
        IEnumerable<Employee> employees, [EnumeratorCancellation] CancellationToken ct = default)
    {
        foreach (var employee in employees)
        {
            var passport = await passportRepository.GetPassport(employee.PassportId, ct) ??
                           throw new PassportNotFoundException(employee.PassportId);
            var department = await departmentRepository.GetDepartment(employee.DepartmentId, ct) ??
                             throw new DepartmentNotFoundException(employee.DepartmentId);
            yield return employee.MapToDto(passport, department);
        }
    }
}