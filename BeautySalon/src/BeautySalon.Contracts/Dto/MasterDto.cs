using BeautySalon.Domain.Enums;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Мастер салона (для чтения)
/// </summary>
public class MasterDto : IHasId
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Полное имя
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Пол
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public string PassportNumber { get; set; } = string.Empty;

    /// <summary>
    /// Специализация
    /// </summary>
    public Specialization Specialization { get; set; }

    /// <summary>
    /// Стаж работы (лет)
    /// </summary>
    public int WorkExperience { get; set; }
}