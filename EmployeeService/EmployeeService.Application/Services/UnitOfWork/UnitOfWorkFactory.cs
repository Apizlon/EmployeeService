using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.Application.Services.UnitOfWork;

public class UnitOfWorkFactory(IServiceProvider serviceProvider) : IUnitOfWorkFactory
{
    public IUnitOfWork Create()
    {
        var connection = serviceProvider.GetRequiredService<IDbConnection>();
        return new UnitOfWork(connection, serviceProvider);
    }
}