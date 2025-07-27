using EmployeeService.Application.Interfaces.Repositories;
using EmployeeService.Application.Interfaces.UnitOfWork;
using EmployeeService.DataAccess.Repositories;
using EmployeeService.DataAccess.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.DataAccess.Extensions;

/// <summary>
/// Методы расширения для репозиториев.
/// </summary>
public static class RepositoryExtension
{
    /// <summary>
    /// Добавление репозиториев.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns><see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPassportRepository, PassportRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }

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
}