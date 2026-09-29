namespace BeautySalon.Domain;

/// <summary>
/// Класс услуг салона красоты
/// </summary>
public class Service 
{
    /// <summary>
    /// Название услуги
    /// </summary>
    public required string Name { get; set; }
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