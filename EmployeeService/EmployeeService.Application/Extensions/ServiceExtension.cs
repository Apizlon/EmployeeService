using EmployeeService.Application.Services.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.Application.Extensions;

public static class ServiceExtension
{
    /// <summary>
    /// Добавление репозиториев.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns><see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();

        return services;
    }
}