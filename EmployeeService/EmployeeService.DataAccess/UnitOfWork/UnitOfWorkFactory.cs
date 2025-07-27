using System.Data;
using EmployeeService.Application.Interfaces.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.DataAccess.UnitOfWork;

/// <inheritdoc/>
public class UnitOfWorkFactory(IServiceProvider serviceProvider) : IUnitOfWorkFactory
{
    /// <summary>
    /// <see cref="IServiceProvider"/>
    /// </summary>
    private readonly IServiceProvider _serviceProvider =
        serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    /// <inheritdoc/>
    public IUnitOfWork Create(OnDispose onDispose = OnDispose.Commit)
    {
        var scope = _serviceProvider.CreateScope();
        var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();

        return new UnitOfWork(connection, scope.ServiceProvider, scope, onDispose);
    }
}