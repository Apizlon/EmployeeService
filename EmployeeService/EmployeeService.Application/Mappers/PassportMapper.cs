using EmployeeService.Application.Contracts;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Mappers;

/// <summary>
/// Маппинг сущности паспорта.
/// </summary>
public static class PassportMapper
{
    /// <summary>
    /// Привести к доменной сущности.
    /// </summary>
    /// <param name="passport"><see cref="PassportDto"/>.</param>
    /// <param name="passportId">Идентификатор паспорта. Используется для обновления.</param>
    /// <returns><see cref="Passport"/>.</returns>
    public static Passport MapToDomain(this PassportDto passport, int passportId = 0) =>
        new()
        {
            Id = passportId,
            Type = passport.Type,
            Number = passport.Number,
        };
}