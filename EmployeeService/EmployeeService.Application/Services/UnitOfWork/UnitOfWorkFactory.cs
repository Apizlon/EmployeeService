using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.Application.Services.UnitOfWork;

public class UnitOfWorkFactory(IServiceProvider serviceProvider) : IUnitOfWorkFactory
{
    public IUnitOfWork Create(OnDispose onDispose = OnDispose.Commit)
    {
        var connection = serviceProvider.GetRequiredService<IDbConnection>();
        return new UnitOfWork(connection, serviceProvider, onDispose);
    }
}