namespace BeautySalon.Contracts.Exceptions;

/// <summary>
/// Выбрасывается, когда в переданных данных указан идентификатор несуществующей связанной сущности
/// </summary>
public class ReferenceNotFoundException(string entityName, int id)
    : Exception($"Связанная сущность «{entityName}» с идентификатором {id} не найдена.")
{
    /// <summary>
    /// Название связанной сущности
    /// </summary>
    public string EntityName { get; } = entityName;

    /// <summary>
    /// Идентификатор, который не удалось найти
    /// </summary>
    public int EntityId { get; } = id;
}