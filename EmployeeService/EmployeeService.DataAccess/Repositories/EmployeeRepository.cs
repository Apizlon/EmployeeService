using System.Data;
using Dapper;
using EmployeeService.DataAccess.SqlScripts;
using EmployeeService.Domain.Entities;

namespace EmployeeService.DataAccess.Repositories;

/// <inheritdoc/>
public class EmployeeRepository : IEmployeeRepository
{
    /// <summary><see cref="IDbConnection"/>.</summary>
    private readonly IDbConnection _connection;

    /// <summary><see cref="IDbTransaction"/>.</summary>
    private IDbTransaction? _transaction;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="connection"><see cref="IDbConnection"/>.</param>
    public EmployeeRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    /// <summary>
    /// Установка транзакции.
    /// </summary>
    /// <param name="transaction"><see cref="IDbTransaction"/>.</param>
    public void SetTransaction(IDbTransaction transaction)
    {
        _transaction = transaction;
    }

    /// <inheritdoc/>
    public async Task<int> AddEmployee(Employee employee)
    {
        var id = await _connection.ExecuteScalarAsync<int>(new(Sql.AddEmployee, employee, _transaction));
        return id;
    }

    /// <inheritdoc/>
    public async Task<Employee?> GetEmployee(int id, CancellationToken ct = default)
    {
        var employee =
            await _connection.QuerySingleOrDefaultAsync<Employee>(new(Sql.GetEmployee, id, _transaction,
                cancellationToken: ct));
        return employee;
    }

    /// <inheritdoc/>
    public async Task<bool> EmployeeExists(int id, CancellationToken ct = default)
    {
        var exists =
            await _connection.ExecuteScalarAsync<bool>(new(Sql.EmployeeExists, id, _transaction,
                cancellationToken: ct));
        return exists;
    }

    /// <inheritdoc/>
    public async Task DeleteEmployee(int id)
    {
        await _connection.ExecuteAsync(Sql.DeleteEmployee, id, _transaction);
    }

    /// <inheritdoc/>
    public async Task UpdateEmployee(int id, Employee employee)
    {
        await _connection.ExecuteAsync(new(Sql.UpdateEmployee, employee, _transaction));
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        var employees = await _connection.QueryAsync<Employee>(new(Sql.GetAllEmployees, _transaction));
        return employees;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Employee>> GetEmployeesByCompanyId(int companyId)
    {
        var employees = await _connection.QueryAsync<Employee>(new(Sql.GetAllEmployees, companyId, _transaction));
        return employees;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentId(int departmentId)
    {
        var employees = await _connection.QueryAsync<Employee>(new(Sql.GetAllEmployees, departmentId, _transaction));
        return employees;
    }
}