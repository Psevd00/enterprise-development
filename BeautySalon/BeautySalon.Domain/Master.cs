namespace BeautySalon.Domain;

/// <summary>
/// Класс данных для мастера салона красоты
/// </summary>
public class Master : Person 
{
    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }
    /// <summary>
    /// Специализация
    /// </summary>
    public required Specialization Specialization { get; set; }
    /// <summary>
    /// Опыт работы
    /// </summary>
    public int WorkExperience { get; set; }
}