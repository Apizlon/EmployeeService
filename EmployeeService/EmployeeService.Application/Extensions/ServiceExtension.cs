using EmployeeService.Application.Interfaces.Services;
using EmployeeService.Application.Interfaces.UnitOfWork;
using EmployeeService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.Application.Extensions;

/// <summary>
/// Методы расширения сервисов.
/// </summary>
public static class ServiceExtension
{
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