using System.Data;
using Dapper;
using EmployeeService.DataAccess.SqlScripts;
using EmployeeService.Domain.Entities;

namespace EmployeeService.DataAccess.Repositories;

/// <inheritdoc/>
public class CompanyRepository : ICompanyRepository
{
    /// <summary><see cref="IDbConnection"/>.</summary>
    private readonly IDbConnection _connection;

    /// <summary><see cref="IDbTransaction"/>.</summary>
    private IDbTransaction? _transaction;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="connection"><see cref="IDbConnection"/>.</param>
    public CompanyRepository(IDbConnection connection)
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
    public async Task<int> AddCompanyAsync(Company company)
    {
        var id = await _connection.ExecuteScalarAsync<int>(new(Sql.AddCompany, company, _transaction));
        return id;
    }

    /// <inheritdoc/>
    public async Task<Company?> GetCompanyAsync(int id, CancellationToken ct = default)
    {
        var company =
            await _connection.QuerySingleOrDefaultAsync<Company>(new(Sql.GetCompany, new { Id = id }, _transaction,
                cancellationToken: ct));
        return company;
    }

    /// <inheritdoc/>
    public async Task<bool> CompanyExistsAsync(int id, CancellationToken ct = default)
    {
        var exists =
            await _connection.ExecuteScalarAsync<bool>(new(Sql.CompanyExists, new { Id = id }, _transaction,
                cancellationToken: ct));
        return exists;
    }

    /// <inheritdoc/>
    public async Task DeleteCompanyAsync(int id)
    {
        await _connection.ExecuteAsync(Sql.DeleteCompany, new { Id = id }, _transaction);
    }

    /// <inheritdoc/>
    public async Task UpdateCompanyAsync(Company company)
    {
        await _connection.ExecuteAsync(new(Sql.UpdateCompany, company, _transaction));
    }
}