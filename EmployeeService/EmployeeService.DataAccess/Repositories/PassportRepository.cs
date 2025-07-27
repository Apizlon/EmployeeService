using System.Data;
using Dapper;
using EmployeeService.DataAccess.SqlScripts;
using EmployeeService.Domain.Entities;

namespace EmployeeService.DataAccess.Repositories;

/// <inheritdoc/>
public class PassportRepository : IPassportRepository
{
    /// <summary><see cref="IDbConnection"/>.</summary>
    private readonly IDbConnection _connection;

    /// <summary><see cref="IDbTransaction"/>.</summary>
    private IDbTransaction? _transaction;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="connection"><see cref="IDbConnection"/>.</param>
    public PassportRepository(IDbConnection connection)
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
    public async Task<int> AddPassportAsync(Passport passport)
    {
        var id = await _connection.ExecuteScalarAsync<int>(new(Sql.AddPassport, passport, _transaction));
        return id;
    }

    /// <inheritdoc/>
    public async Task<Passport?> GetPassportAsync(int id, CancellationToken ct = default)
    {
        var passport =
            await _connection.QuerySingleOrDefaultAsync<Passport>(new(Sql.GetPassport, new { Id = id }, _transaction,
                cancellationToken: ct));
        return passport;
    }

    /// <inheritdoc/>
    public async Task<bool> PassportExistsAsync(int id, CancellationToken ct = default)
    {
        var exists =
            await _connection.ExecuteScalarAsync<bool>(new(Sql.PassportExists, new { Id = id }, _transaction,
                cancellationToken: ct));
        return exists;
    }

    /// <inheritdoc/>
    public async Task DeletePassportAsync(int id)
    {
        await _connection.ExecuteAsync(Sql.DeletePassport, new { Id = id }, _transaction);
    }

    /// <inheritdoc/>
    public async Task UpdatePassportAsync(Passport passport)
    {
        await _connection.ExecuteAsync(new(Sql.UpdatePassport, passport, _transaction));
    }
}