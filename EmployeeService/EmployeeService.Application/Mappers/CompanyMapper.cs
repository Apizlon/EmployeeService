using EmployeeService.Application.Contracts.Company;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Mappers;

public static class CompanyMapper
{
    public static Company MapToDomain(this CompanyRequest companyDto, int id = 0) =>
        new()
        {
            Id = id,
            Name = companyDto.Name,
        };


    public static CompanyResponse MapToDto(this Company company) =>
        new()
        {
            Id = company.Id,
            Name = company.Name,
        };
}