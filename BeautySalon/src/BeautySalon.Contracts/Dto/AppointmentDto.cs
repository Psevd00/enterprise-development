namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Запись на приём (для чтения)
/// </summary>
public class AppointmentDto : IHasId
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Полное имя клиента
    /// </summary>
    public string ClientFullName { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор мастера
    /// </summary>
    public int MasterId { get; set; }

    /// <summary>
    /// Полное имя мастера
    /// </summary>
    public string MasterFullName { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    public int ServiceId { get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public string ServiceName { get; set; } = string.Empty;

    /// <summary>
    /// Дата и время приёма
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Признак постоянного клиента
    /// </summary>
    public bool IsRegularClient { get; set; }
}