using System.Data;
using Dapper;
using EmployeeService.DataAccess.SqlScripts;
using EmployeeService.Domain.Entities;

namespace EmployeeService.DataAccess.Repositories;

/// <inheritdoc/>
public class PassportRepository : IPassportRepository
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
    public PassportRepository(IDbConnection connection, IDbTransaction transaction)
    {
        _connection = connection;
        _transaction = transaction;
    }

    /// <inheritdoc/>
    public async Task<int> AddPassport(Passport passport)
    {
        var id = await _connection.ExecuteScalarAsync<int>(new(Sql.AddPassport, passport, _transaction));
        return id;
    }

    /// <inheritdoc/>
    public async Task<Passport?> GetPassport(int id, CancellationToken ct = default)
    {
        var passport =
            await _connection.QuerySingleOrDefaultAsync<Passport>(new(Sql.GetPassport, id, _transaction,
                cancellationToken: ct));
        return passport;
    }

    /// <inheritdoc/>
    public async Task<bool> PassportExists(int id, CancellationToken ct = default)
    {
        var exists =
            await _connection.ExecuteScalarAsync<bool>(new(Sql.PassportExists, id, _transaction,
                cancellationToken: ct));
        return exists;
    }

    /// <inheritdoc/>
    public async Task DeletePassport(int id)
    {
        await _connection.ExecuteAsync(Sql.DeletePassport, id, _transaction);
    }

    /// <inheritdoc/>
    public async Task UpdatePassport(int id, Passport passport)
    {
        await _connection.ExecuteAsync(new(Sql.UpdatePassport, passport, _transaction));
    }
}