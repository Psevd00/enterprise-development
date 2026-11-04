using BeautySalon.Contracts.Dto;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Аналитические запросы по данным салона красоты
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Мастера со стажем не менее указанного
    /// </summary>
    Task<IReadOnlyList<MasterDto>> GetExperiencedMastersAsync(
        int minExperienceYears = 5, CancellationToken cancellationToken = default);

    /// <summary>
    /// Свободные окна мастера на выбранную дату (рабочий день 09 до 18)
    /// </summary>
    Task<IReadOnlyList<TimeSlotDto>?> GetFreeSlotsAsync(
        int masterId, DateOnly date, CancellationToken cancellationToken = default);

    /// <summary>
    /// Топ наиболее популярных услуг по количеству записей
    /// </summary>
    Task<IReadOnlyList<PopularServiceDto>> GetPopularServicesAsync(
        int top = 5, CancellationToken cancellationToken = default);

    /// <summary>
    /// Клиенты, записавшиеся более одного раза в выбранном месяце
    /// </summary>
    Task<IReadOnlyList<RepeatClientDto>> GetRepeatClientsAsync(
        int year, int month, CancellationToken cancellationToken = default);

    /// <summary>
    /// Клиенты, записанные к нескольким мастерам, упорядоченные по дате рождения
    /// </summary>
    Task<IReadOnlyList<ClientDto>> GetClientsWithMultipleMastersAsync(
        CancellationToken cancellationToken = default);
}