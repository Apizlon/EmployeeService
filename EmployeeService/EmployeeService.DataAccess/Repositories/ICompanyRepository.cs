using EmployeeService.Domain.Entities;

namespace EmployeeService.DataAccess.Repositories;

/// <summary>
/// Репозиторий компании.
/// </summary>
public interface ICompanyRepository
{
    /// <summary>
    /// Добавить компанию.
    /// </summary>
    /// <param name="company"><see cref="Company"/>.</param>
    /// <returns>Индентификатор компании.</returns>
    public Task<int> AddCompany(Company company);

    /// <summary>
    /// Получить компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="Company"/>>.</returns>
    public Task<Company?> GetCompany(int id, CancellationToken ct = default);

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
    /// <param name="company"><see cref="Company"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task UpdateCompany(int id, Company company);
}