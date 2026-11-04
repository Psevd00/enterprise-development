using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Application.Services;

/// <summary>
/// Базовая реализация CRUD-сервиса: работает с репозиторием и отдаёт только DTO
/// </summary>
public abstract class CrudService<TEntity, TDto, TEditDto>(IRepository<TEntity> repository)
    : ICrudService<TDto, TEditDto>
    where TEntity : class, IEntity
    where TDto : class, IHasId
    where TEditDto : class
{
    /// <summary>
    /// Репозиторий сущности
    /// </summary>
    protected IRepository<TEntity> Repository { get; } = repository;

    /// <summary>
    /// Преобразует сущность в DTO для чтения
    /// </summary>
    protected abstract TDto ToDto(TEntity entity);

    /// <summary>
    /// Создаёт новую сущность из DTO
    /// </summary>
    protected abstract Task<TEntity> BuildAsync(TEditDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Применяет данные из DTO к существующей сущности
    /// </summary>
    protected abstract Task ApplyAsync(TEntity entity, TEditDto dto, CancellationToken cancellationToken);

    public async Task<IReadOnlyList<TDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await Repository.GetAllAsync(cancellationToken);
        return items.Select(ToDto).ToList();
    }

    public async Task<TDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await Repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<TDto> CreateAsync(TEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await BuildAsync(dto, cancellationToken);
        var created = await Repository.AddAsync(entity, cancellationToken);
        return ToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, TEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await Repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        await ApplyAsync(entity, dto, cancellationToken);
        return await Repository.UpdateAsync(entity, cancellationToken);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        Repository.DeleteAsync(id, cancellationToken);
}