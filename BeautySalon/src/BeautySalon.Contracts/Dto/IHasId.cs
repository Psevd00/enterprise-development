namespace BeautySalon.Contracts.Dto;

/// <summary>
/// DTO с идентификатором
/// </summary>
public interface IHasId
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    int Id { get; }
}