namespace BeautySalon.Domain;

/// <summary>
/// Класс данных для записи на прием в салон красоты
/// </summary>
public class Appointment 
{
    /// <summary>
    /// Мастер
    /// </summary>
    public required Master Master { get; set; }
    /// <summary>
    /// Клиент
    /// </summary>
    public required Client Client { get; set; }
    /// <summary>
    /// Услуга
    /// </summary>
    public required Service Service { get; set; }
    /// <summary>
    /// Дата и время приема
    /// </summary>
    public DateTime DateTime { get; set; }
    /// <summary>
    /// Признак постоянного клиента
    /// </summary>
    public bool IsRegularClient { get; set; }
}