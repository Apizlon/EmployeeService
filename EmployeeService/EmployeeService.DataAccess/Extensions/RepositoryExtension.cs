using EmployeeService.DataAccess.Repositories;
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
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IPassportRepository, PassportRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        return services;
    }
}