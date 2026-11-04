using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Запись на приём (для создания и изменения)
/// </summary>
public class AppointmentEditDto
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Range(1, int.MaxValue)]
    public int ClientId { get; set; }

    /// <summary>
    /// Идентификатор мастера
    /// </summary>
    [Range(1, int.MaxValue)]
    public int MasterId { get; set; }

    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    [Range(1, int.MaxValue)]
    public int ServiceId { get; set; }

    /// <summary>
    /// Дата и время приёма
    /// </summary>
    [Required]
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Признак постоянного клиента
    /// </summary>
    public bool IsRegularClient { get; set; }
}
