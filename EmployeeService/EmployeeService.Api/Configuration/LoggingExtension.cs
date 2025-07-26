using Serilog;

namespace EmployeeService.Api.Configuration;

/// <summary>
/// Класс расширений для логирования.
/// </summary>
public static class LoggingExtension
{
    /// <summary>
    /// Настройка Serilog.
    /// </summary>
    /// <param name="hostBuilder"><see cref="IHostBuilder"/>.</param>
    /// <returns><see cref="IHostBuilder"/>.</returns>
    public static IHostBuilder ConfigureSerilog(this IHostBuilder hostBuilder)
    {
        return hostBuilder.UseSerilog((context, configuration) =>
        {
            configuration
                .MinimumLevel.Debug()
                .WriteTo.Console();
        });
    }
}