using System.Data;
using Dapper;
using EmployeeService.DataAccess.SqlScripts;
using EmployeeService.Domain.Entities;

namespace EmployeeService.DataAccess.Repositories;

/// <inheritdoc/>
public class DepartmentRepository : IDepartmentRepository
{
    /// <summary><see cref="IDbConnection"/>.</summary>
    private readonly IDbConnection _connection;

    /// <summary><see cref="IDbTransaction"/>.</summary>
    private IDbTransaction? _transaction;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="connection"><see cref="IDbConnection"/>.</param>
    public DepartmentRepository(IDbConnection connection)
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
    public async Task<int> AddDepartment(Department department)
    {
        var id = await _connection.ExecuteScalarAsync<int>(new(Sql.AddDepartment, department, _transaction));
        return id;
    }

    /// <inheritdoc/>
    public async Task<Department?> GetDepartment(int id, CancellationToken ct = default)
    {
        var department =
            await _connection.QuerySingleOrDefaultAsync<Department>(new(Sql.GetDepartment, new { Id = id }, _transaction,
                cancellationToken: ct));
        return department;
    }

    /// <inheritdoc/>
    public async Task<bool> DepartmentExists(int id, CancellationToken ct = default)
    {
        var exists =
            await _connection.ExecuteScalarAsync<bool>(new(Sql.DepartmentExists, new { Id = id }, _transaction,
                cancellationToken: ct));
        return exists;
    }

    /// <inheritdoc/>
    public async Task DeleteDepartment(int id)
    {
        await _connection.ExecuteAsync(Sql.DeleteDepartment, new { Id = id }, _transaction);
    }

    /// <inheritdoc/>
    public async Task UpdateDepartment(Department department)
    {
        await _connection.ExecuteAsync(new(Sql.UpdateDepartment, department, _transaction));
    }
}