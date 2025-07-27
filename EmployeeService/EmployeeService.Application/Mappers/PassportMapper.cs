using EmployeeService.Application.Contracts;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Mappers;

public static class PassportMapper
{
    public static Passport MapToDomain(this PassportDto passport) =>
        new()
        {
            Type = passport.Type,
            Number = passport.Number,
        };
    
    public static PassportDto MapToDomain(this Passport passport) =>
        new()
        {
            Type = passport.Type,
            Number = passport.Number,
        };
}