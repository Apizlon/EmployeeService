using System.Data;
using EmployeeService.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.Application.Services.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private IDbTransaction? _transaction;
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceScope _scope;
    private bool _disposed;
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

    public void BeginTransaction()
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("Transaction already started.");
        }

        _transaction = _connection.BeginTransaction();
    }

    public void Commit()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction to commit.");
        }

        _transaction.Commit();
        _transaction = null;
    }

    public void Rollback()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction to rollback.");
        }

        _transaction.Rollback();
        _transaction = null;
    }

    public T GetRepository<T>() where T : class
    {
        var repository = _serviceProvider.GetRequiredService<T>();

        if (_transaction != null && repository is ITransactionalRepository transactionalRepo)
        {
            transactionalRepo.SetTransaction(_transaction);
        }

        return repository;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            if (_transaction is not null && _onDispose == OnDispose.Rollback)
            {
                _transaction.Rollback();
            }

            _transaction?.Dispose();
            _connection.Dispose();
            _scope.Dispose();
            _disposed = true;
        }
    }
}