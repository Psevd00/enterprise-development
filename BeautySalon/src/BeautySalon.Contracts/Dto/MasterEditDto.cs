using System.ComponentModel.DataAnnotations;
using BeautySalon.Domain.Enums;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Мастер салона (для создания и изменения)
/// </summary>
public class MasterEditDto
{
    /// <summary>
    /// Полное имя
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Пол
    /// </summary>
    [EnumDataType(typeof(Gender))]
    public Gender Gender { get; set; }

    /// <summary>
    /// Номер паспорта в формате «1234 567890»
    /// </summary>
    [Required]
    [RegularExpression(@"^\d{4} \d{6}$", ErrorMessage = "Номер паспорта должен иметь формат «1234 567890»")]
    public string PassportNumber { get; set; } = string.Empty;

    /// <summary>
    /// Специализация
    /// </summary>
    [EnumDataType(typeof(Specialization))]
    public Specialization Specialization { get; set; }

    /// <summary>
    /// Стаж работы (лет)
    /// </summary>
    [Range(0, 80)]
    public int WorkExperience { get; set; }
}