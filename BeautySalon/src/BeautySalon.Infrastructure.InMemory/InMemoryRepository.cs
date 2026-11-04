using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Базовый репозиторий поверх коллекции в памяти
/// </summary>
public abstract class InMemoryRepository<T>(List<T> items) : IRepository<T>
    where T : class, IEntity
{
    private readonly object _sync = new();

    public Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            return Task.FromResult<IReadOnlyList<T>>(items.ToList());
        }
    }

    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            return Task.FromResult(items.FirstOrDefault(x => x.Id == id));
        }
    }

    public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            if (entity.Id == 0)
            {
                entity.Id = items.Count != 0 ? items.Max(x => x.Id) + 1 : 1;
            }
            else if (items.Any(x => x.Id == entity.Id))
            {
                throw new InvalidOperationException($"Сущность с идентификатором {entity.Id} уже существует.");
            }

            items.Add(entity);
            return Task.FromResult(entity);
        }
    }

    public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            var index = items.FindIndex(x => x.Id == entity.Id);
            if (index == -1)
            {
                return Task.FromResult(false);
            }

            items[index] = entity;
            return Task.FromResult(true);
        }
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            var item = items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item is not null && items.Remove(item));
        }
    }
}