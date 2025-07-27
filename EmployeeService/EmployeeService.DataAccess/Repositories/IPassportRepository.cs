using EmployeeService.Domain.Entities;

namespace EmployeeService.DataAccess.Repositories;

/// <summary>
/// Репозиторий паспорта.
/// </summary>
public interface IPassportRepository : ITransactionalRepository
{
    /// <summary>
    /// Добавить паспорт.
    /// </summary>
    /// <param name="passport"><see cref="Passport"/>.</param>
    /// <returns>Индентификатор паспорта.</returns>
    public Task<int> AddPassportAsync(Passport passport);

    /// <summary>
    /// Получить паспорт.
    /// </summary>
    /// <param name="id">Идентификатор паспорта.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="Passport"/>>.</returns>
    public Task<Passport?> GetPassportAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Существует ли паспорт.
    /// </summary>
    /// <param name="id">Идентификатор паспорта.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns>true если существует, иначе - false</returns>
    public Task<bool> PassportExistsAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Удалить паспорт.
    /// </summary>
    /// <param name="id">Идентификатор паспорта.</param>
    /// <returns><see cref="Task"/></returns>
    public Task DeletePassportAsync(int id);

    /// <summary>
    /// Обновить паспорт.
    /// </summary>
    /// <param name="passport"><see cref="Passport"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task UpdatePassportAsync(Passport passport);
}