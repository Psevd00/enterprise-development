namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Популярная услуга с количеством записей
/// </summary>
public class PopularServiceDto
{
    /// <summary>
    /// Название услуги
    /// </summary>
    public string ServiceName { get; set; } = string.Empty;

    /// <summary>
    /// Количество записей на услугу
    /// </summary>
    public int AppointmentsCount { get; set; }
}