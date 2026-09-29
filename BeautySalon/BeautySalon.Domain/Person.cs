namespace BeautySalon.Domain;

/// <summary>
/// Класс данных для представления человека
/// </summary>
public abstract class Person 
{
    /// <summary>
    /// Полное имя
    /// </summary>
    public required string FullName { get; set; }
    /// <summary>
    /// Пол
    /// </summary>
    public Gender Gender { get; set; }
}