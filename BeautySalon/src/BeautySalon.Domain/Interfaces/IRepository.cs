namespace BeautySalon.Domain.Interfaces;

/// <summary>
/// Базовый интерфейс репозитория с CRUD-операциями над сущностью
/// </summary>
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Получает все сущности
    /// </summary>
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает сущность по идентификатору или <c>null</c>, если она не найдена
    /// </summary>
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет новую сущность. Если идентификатор не задан (0), он назначается автоматически
    /// </summary>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Сохраняет изменения существующей сущности
    /// </summary>
    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}