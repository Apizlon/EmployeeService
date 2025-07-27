using EmployeeService.Application.Contracts.Department;
using EmployeeService.Domain.Exceptions;

namespace EmployeeService.Application.Validators;

/// <summary>
/// Валидатор отдела компании.
/// </summary>
public static class DepartmentValidator
{
    /// <summary>
    /// Валидация при добавлении отдела.
    /// </summary>
    /// <param name="department"><see cref="AddDepartmentRequest"/>.</param>
    /// <param name="companyExists">Существует ли компания.</param>
    /// <returns><see cref="AddDepartmentRequest"/>.</returns>
    /// <exception cref="CompanyNotFoundException">Компания не найдена.</exception>
    public static AddDepartmentRequest ValidateAdd(this AddDepartmentRequest department, bool companyExists)
    {
        if (!companyExists) throw new CompanyNotFoundException(department.CompanyId);
        ValidateName(department.Name!);
        ValidatePhone(department.Phone!);

        return department;
    }

    /// <summary>
    /// Валидация про добавлении.
    /// </summary>
    /// <param name="department"><see cref="UpdateDepartmentRequest"/>.</param>
    /// <param name="companyExists">Существует ли компания.</param>
    /// <returns><see cref="UpdateDepartmentRequest"/>.</returns>
    /// <exception cref="CompanyNotFoundException">Компания не найдена.</exception>
    /// <exception cref="BadRequestException">Ошибка пользовательских даненых.</exception>
    /// <remarks>Если поле null, оно не обновляется.</remarks>>
    public static UpdateDepartmentRequest ValidateUpdate(this UpdateDepartmentRequest department, bool companyExists)
    {
        if (!companyExists) throw new CompanyNotFoundException(department.CompanyId!.Value);
        if (department.Name is not null)
        {
            ValidateName(department.Name);
        }

        if (department.Phone is not null)
        {
            ValidatePhone(department.Phone);
        }

        return department;
    }

    private static void ValidateName(string name)
    {
        ValidatorUtils.WhiteSpaceCheck(name, "Department Name");
        ValidatorUtils.LengthCheck(name, 255, "Department Name");
    }

    private static void ValidatePhone(string phone)
    {
        ValidatorUtils.DigitsSpacesPlusCheck(phone, "Department Phone");
        ValidatorUtils.LengthCheck(phone, 50, "Department Phone");
    }
}