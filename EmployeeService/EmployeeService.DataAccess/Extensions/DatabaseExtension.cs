using System.Data;
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Serilog;

namespace EmployeeService.DataAccess.Extensions;

/// <summary>
/// Расширения для работы с базой данных.
/// </summary>
public static class DatabaseExtension
{
    private const string ConnectionStringKey = "EmployeeDatabaseConnection";

    /// <summary>
    /// Настройка подключения к базе данных, проверка существования базы и выполнение миграций.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <param name="configuration"><see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IServiceCollection"/>.</returns>
    /// <remarks>Регистрируется IDbConnection в DI для использования в UnitOfWork.</remarks>
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString(ConnectionStringKey);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException($"Error obtaining connection string {ConnectionStringKey}");
        }

        services.AddScoped<IDbConnection>(x => new NpgsqlConnection(connectionString));
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        Log.Debug("Connection string obtained: {ConnectionString}", connectionString);

        EnsureDatabaseExists(connectionString);
        MigrateDatabase(services.BuildServiceProvider());

        return services;
    }

    private static void EnsureDatabaseExists(string connectionString)
    {
        var builder = new NpgsqlConnectionStringBuilder(connectionString);
        var databaseName = builder.Database;
        var masterConnectionString = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Database = "postgres"
        }.ConnectionString;

        using var connection = new NpgsqlConnection(masterConnectionString);
        try
        {
            connection.Open();
            Log.Debug("Connected to 'postgres' db.");
        }
        catch (NpgsqlException ex)
        {
            Log.Error(ex, "Error connecting to 'postgres'.");
            throw;
        }

        using var command = new NpgsqlCommand(
            $"SELECT 1 FROM pg_database WHERE datname = @dbName", connection);
        command.Parameters.AddWithValue("dbName", databaseName);

        var exists = command.ExecuteScalar() != null;
        Log.Debug($"Database '{databaseName}' exists: {exists}");

        if (!exists)
        {
            Log.Information($"Creating database '{databaseName}'.");
            using var createCommand = new NpgsqlCommand(
                $"CREATE DATABASE \"{databaseName}\"", connection);
            createCommand.ExecuteNonQuery();
            Log.Information($"Database '{databaseName}' successfully created.");
        }
    }

    private static void MigrateDatabase(IServiceProvider serviceProvider)
    {
        Log.Information("Starting migrations.");
        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        try
        {
            runner.MigrateUp();
            Log.Information("Migrations successfully completed.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while migrating data.");
            throw;
        }
    }
}