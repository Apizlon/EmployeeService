using System.Data;
using EmployeeService.Application.Interfaces.Repositories;
using EmployeeService.Application.Interfaces.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.DataAccess.UnitOfWork;

/// <inheritdoc/>
public class UnitOfWork : IUnitOfWork
{
    /// <summary><see cref="IDbConnection"/>.</summary>
    private readonly IDbConnection _connection;
    /// <summary><see cref="IDbTransaction"/>.</summary>
    private IDbTransaction? _transaction;
    /// <summary><see cref="IServiceProvider"/>.</summary>
    private readonly IServiceProvider _serviceProvider;
    /// <summary><see cref="IServiceScope"/>.</summary>
    private readonly IServiceScope _scope;
    /// <summary>
    /// Освобожен ли ресурс.
    /// </summary>
    private bool _disposed;
    /// <summary><see cref="OnDispose"/>.</summary>
    private readonly OnDispose _onDispose;

    public UnitOfWork(
        IDbConnection connection,
        IServiceProvider serviceProvider,
        IServiceScope scope,
        OnDispose onDispose = OnDispose.Commit)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _scope = scope ?? throw new ArgumentNullException(nameof(scope));
        _onDispose = onDispose;
        _connection.Open();
    }

    /// <inheritdoc/>
    public void BeginTransaction()
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("Transaction already started.");
        }

        _transaction = _connection.BeginTransaction();
    }

    /// <inheritdoc/>
    public void Commit()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction to commit.");
        }

        _transaction.Commit();
        _transaction = null;
    }

    /// <inheritdoc/>
    public void Rollback()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction to rollback.");
        }

        _transaction.Rollback();
        _transaction = null;
    }

    /// <inheritdoc/>
    public T GetRepository<T>() where T : class
    {
        var repository = _serviceProvider.GetRequiredService<T>();

        if (_transaction != null && repository is ITransactionalRepository transactionalRepo)
        {
            transactionalRepo.SetTransaction(_transaction);
        }

        return repository;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (!_disposed)
        {
            if (_transaction is not null && _onDispose == OnDispose.Rollback)
            {
                _transaction.Rollback();
            }
            
            if (_transaction is not null && _onDispose == OnDispose.Commit)
            {
                _transaction.Commit();
            }

            _transaction?.Dispose();
            _connection.Dispose();
            _scope.Dispose();
            _disposed = true;
        }
    }
}