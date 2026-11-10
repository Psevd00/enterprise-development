namespace BeautySalon.Application.Exceptions;

/// <summary>
/// Запрошенная сущность не найдена (в API превращается в ответ 404)
/// </summary>
/// <param name="entityName">Название сущности</param>
/// <param name="id">Идентификатор, который не нашли</param>
public class NotFoundException(string entityName, int id)
    : Exception($"{entityName} с идентификатором {id} не найден(а).")
{
    /// <summary>
    /// Название сущности
    /// </summary>
    public string EntityName { get; } = entityName;

    /// <summary>
    /// Идентификатор, который не удалось найти
    /// </summary>
    public int EntityId { get; } = id;
}
