namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Свободное окно в расписании мастера
/// </summary>
public class TimeSlotDto
{
    /// <summary>
    /// Начало окна
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// Конец окна
    /// </summary>
    public DateTime End { get; set; }
}