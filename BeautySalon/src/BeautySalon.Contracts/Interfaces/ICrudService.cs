using BeautySalon.Contracts.Dto;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Базовый сервис с CRUD-операциями над DTO
/// </summary>
public interface ICrudService<TDto, in TEditDto>
    where TDto : class, IHasId
    where TEditDto : class
{
    /// <summary>
    /// Получает все записи
    /// </summary>
    Task<IReadOnlyList<TDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает запись по идентификатору или <c>null</c>, если она не найдена
    /// </summary>
    Task<TDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт новую запись
    /// </summary>
    Task<TDto> CreateAsync(TEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет запись
    /// </summary>
    Task<bool> UpdateAsync(int id, TEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет запись
    /// </summary>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}