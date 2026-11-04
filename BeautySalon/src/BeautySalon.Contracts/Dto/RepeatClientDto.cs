namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Клиент с повторными записями за период
/// </summary>
public class RepeatClientDto
{
    /// <summary>
    /// Клиент
    /// </summary>
    public ClientDto Client { get; set; } = new();

    /// <summary>
    /// Общее количество записей клиента за период
    /// </summary>
    public int AppointmentsCount { get; set; }
}