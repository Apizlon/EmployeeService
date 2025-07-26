using EmployeeService.Domain.Entities;

namespace EmployeeService.DataAccess.Repositories;

/// <summary>
/// Репозиторий паспорта.
/// </summary>
public interface IPassportRepository
{
    /// <summary>
    /// Добавить паспорт.
    /// </summary>
    /// <param name="passport"><see cref="Passport"/>.</param>
    /// <returns>Индентификатор паспорта.</returns>
    public Task<int> AddPassport(Passport passport);
    
    /// <summary>
    /// Получить паспорт.
    /// </summary>
    /// <param name="id">Идентификатор паспорта.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="Passport"/>>.</returns>
    public Task<Passport?> GetPassport(int id, CancellationToken ct = default);

    /// <summary>
    /// Существует ли паспорт.
    /// </summary>
    /// <param name="id">Идентификатор паспорта.</param>
    /// <param name="ct"><see cref="CancellationToken"/>.</param>
    /// <returns>true если существует, иначе - false</returns>
    public Task<bool> PassportExists(int id, CancellationToken ct = default);

    /// <summary>
    /// Удалить паспорт.
    /// </summary>
    /// <param name="id">Идентификатор паспорта.</param>
    /// <returns><see cref="Task"/></returns>
    public Task DeletePassport(int id);
    
    /// <summary>
    /// Обновить паспорт.
    /// </summary>
    /// <param name="id">Идентификатор паспорта.</param>
    /// <param name="passport"><see cref="Passport"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task UpdatePassport(int id, Passport passport);
}