using EmployeeService.Application.Services;
using EmployeeService.Application.Services.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.Application.Extensions;

/// <summary>
/// Методы расширения сервисов.
/// </summary>
public static class ServiceExtension
{
    /// <summary>
    /// Добавление UnitOfWork.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns><see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddUnitOfWorkFactory(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
        return services;
    }

    /// <summary>
    /// Добавление сервисов.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns><see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IEmployeeService, Services.EmployeeService>();

        return services;
    }
}