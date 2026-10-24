namespace BeautySalon.Infrastructure.InMemory;

using BeautySalon.Domain;

/// <summary>
/// Обобщенный репозиторий для работы с сущностями в памяти
/// </summary>
public class InMemoryRepository<T> where T : class, IEntity
{
    protected readonly List<T> _items;

    public InMemoryRepository(List<T> items)
    {
        _items = items;
    }

    public virtual IList<T> GetAll() => _items;

    public virtual T? GetById(int id) => _items.FirstOrDefault(x => x.Id == id);

    public virtual T Add(T entity)
    {
        if (entity.Id == 0)
        {
            entity.Id = _items.Count != 0 ? _items.Max(x => x.Id) + 1 : 1;
        }
        _items.Add(entity);
        return entity;
    }

    public virtual T Update(T entity, int id)
    {
        var index = _items.FindIndex(x => x.Id == id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"Сущность с идентификатором {id} не найдена.");
        }

        entity.Id = id;
        _items[index] = entity;
        return entity;
    }

    public virtual bool Delete(int id)
    {
        var item = GetById(id);
        if (item is null)
        {
            return false;
        }

        return _items.Remove(item);
    }
}