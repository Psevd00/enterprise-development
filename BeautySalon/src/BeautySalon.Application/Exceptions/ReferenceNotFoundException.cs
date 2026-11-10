namespace BeautySalon.Application.Exceptions;

/// <summary>
/// В переданных данных указан идентификатор несуществующей связанной сущности (в API превращается в ответ 400)
/// </summary>
/// <param name="entityName">Название связанной сущности</param>
/// <param name="id">Идентификатор, который не нашли</param>
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
