using BeautySalon.Domain;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Базовый обобщенный репозиторий для выполнения CRUD-операций
/// </summary>
public class InMemoryRepository<T> where T : class, IEntity
{
    protected readonly List<T> _items;

    /// <summary>
    /// Инициализирует новый экземпляр базового репозитория переданным списком элементов
    /// </summary>
    public InMemoryRepository(List<T> items)
    {
        _items = items;
    }
    /// <summary>
    /// Получает список всех сущностей
    /// </summary>
    public virtual IList<T> GetAll() => _items;

    /// <summary>
    /// Получает сущность по её уникальному идентификатору
    /// </summary>
    public virtual T? GetById(int id) => _items.FirstOrDefault(x => x.Id == id);

    /// <summary>
    /// Добавляет новую сущность в хранилище
    /// </summary>
    public virtual T Add(T entity)
    {
        if (entity.Id == 0)
        {
            entity.Id = _items.Count != 0 ? _items.Max(x => x.Id) + 1 : 1;
        }
        _items.Add(entity);
        return entity;
    }

    /// <summary>
    /// Обновляет существующую сущность по её идентификатору
    /// </summary>
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

    /// <summary>
    /// Удаляет сущность из хранилища по её уникальному идентификатору
    /// </summary>
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