using EmployeeService.Application.Contracts;
using EmployeeService.Application.Contracts.Company;

namespace EmployeeService.Application.Services;

/// <summary>
/// Сервис для работы с компаниями.
/// </summary>
public interface ICompanyService
{
    /// <summary>
    /// Добавить компанию.
    /// </summary>
    /// <param name="company"><see cref="CompanyRequest"/>.</param>
    /// <returns>Индентификатор компании.</returns>
    public Task<int> AddCompany(CompanyRequest company);

    /// <summary>
    /// Получить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="CompanyRequest"/>>.</returns>
    public Task<CompanyResponse?> GetCompany(int id, CancellationToken ct = default);

    /// <summary>
    /// Существует ли компания.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns>true если существует, иначе - false</returns>
    public Task<bool> CompanyExists(int id, CancellationToken ct = default);

    /// <summary>
    /// Удалить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <returns><see cref="Task"/></returns>
    public Task DeleteCompany(int id);

    /// <summary>
    /// Обновить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="company"><see cref="CompanyRequest"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task UpdateCompany(int id, CompanyRequest company);
}