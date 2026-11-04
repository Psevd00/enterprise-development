using BeautySalon.Domain.Enums;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Услуга салона (для чтения)
/// </summary>
public class ServiceDto : IHasId
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Категория услуги
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Цена услуги
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Продолжительность услуги
    /// </summary>
    public TimeSpan Duration { get; set; }
}