using EmployeeService.Application.Contracts.Company;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Mappers;

/// <summary>
/// Маппер для сущности компании.
/// </summary>
public static class CompanyMapper
{
    /// <summary>
    /// Привести к доменной сущности.
    /// </summary>
    /// <param name="companyDto"><see cref="CompanyRequest"/>.</param>
    /// <param name="id">Идентификатор компании. Передаётся для обновления.</param>
    /// <returns><see cref="Company"/></returns>
    public static Company MapToDomain(this CompanyRequest companyDto, int id = 0) =>
        new()
        {
            Id = id,
            Name = companyDto.Name,
        };

    /// <summary>
    /// Привести к DTO.
    /// </summary>
    /// <param name="company"><see cref="Company"/></param>
    /// <returns><see cref="CompanyResponse"/>.</returns>
    public static CompanyResponse MapToDto(this Company company) =>
        new()
        {
            Id = company.Id,
            Name = company.Name,
        };
}