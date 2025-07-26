using System.Data;
using Dapper;
using EmployeeService.DataAccess.SqlScripts;
using EmployeeService.Domain.Entities;

namespace EmployeeService.DataAccess.Repositories;

/// <inheritdoc/>
public class CompanyRepository : ICompanyRepository
{
    /// <summary> <see cref="IDbConnection"/>.</summary>
    private readonly IDbConnection _connection;

    /// <summary> <see cref="IDbTransaction"/>.</summary>
    private readonly IDbTransaction _transaction;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="connection"><see cref="IDbConnection"/>.</param>
    /// <param name="transaction"><see cref="IDbTransaction"/>.</param>
    public CompanyRepository(IDbConnection connection, IDbTransaction transaction)
    {
        _connection = connection;
        _transaction = transaction;
    }

    /// <inheritdoc/>
    public async Task<int> AddCompany(Company company)
    {
        var id = await _connection.ExecuteScalarAsync<int>(new(Sql.AddCompany, company, _transaction));
        return id;
    }

    /// <inheritdoc/>
    public async Task<Company?> GetCompany(int id, CancellationToken ct = default)
    {
        var company =
            await _connection.QuerySingleOrDefaultAsync<Company>(new(Sql.GetCompany, id, _transaction,
                cancellationToken: ct));
        return company;
    }

    /// <inheritdoc/>
    public async Task<bool> CompanyExists(int id, CancellationToken ct = default)
    {
        var exists =
            await _connection.ExecuteScalarAsync<bool>(new(Sql.CompanyExists, id, _transaction, cancellationToken: ct));
        return exists;
    }

    /// <inheritdoc/>
    public async Task DeleteCompany(int id)
    {
        await _connection.ExecuteAsync(Sql.DeleteCompany, id, _transaction);
    }

    /// <inheritdoc/>
    public async Task UpdateCompany(int id, Company company)
    {
        await _connection.ExecuteAsync(new(Sql.UpdateCompany, company, _transaction));
    }
}