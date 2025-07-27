using EmployeeService.Application.Contracts.Company;

namespace EmployeeService.Application.Interfaces.Services;

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
    public Task<int> AddCompanyAsync(CompanyRequest company);

    /// <summary>
    /// Получить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="CompanyRequest"/>>.</returns>
    public Task<CompanyResponse?> GetCompanyAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Удалить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <returns><see cref="Task"/></returns>
    public Task DeleteCompanyAsync(int id);

    /// <summary>
    /// Обновить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="company"><see cref="CompanyRequest"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task UpdateCompanyAsync(int id, CompanyRequest company);
}