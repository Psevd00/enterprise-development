using System.ComponentModel.DataAnnotations;
using BeautySalon.Domain.Enums;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Услуга салона (для создания и изменения)
/// </summary>
public class ServiceEditDto
{
    /// <summary>
    /// Название услуги
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Категория услуги
    /// </summary>
    [EnumDataType(typeof(Category))]
    public Category Category { get; set; }

    /// <summary>
    /// Цена услуги
    /// </summary>
    [Range(typeof(decimal), "0", "1000000")]
    public decimal Price { get; set; }

    /// <summary>
    /// Продолжительность услуги (от 5 минут до 3 часов), например «01:30:00»
    /// </summary>
    [Range(typeof(TimeSpan), "00:05:00", "03:00:00")]
    public TimeSpan Duration { get; set; }
}