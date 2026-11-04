using System.ComponentModel.DataAnnotations;
using BeautySalon.Domain.Enums;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Клиент салона (для создания и изменения)
/// </summary>
public class ClientEditDto
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
    /// Дата рождения
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Required]
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;
}