using BeautySalon.Domain;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Определяет операции доступа к данным сервисам
/// </summary>
public class ServiceRepository : InMemoryRepository<Service>
{
    public ServiceRepository(BeautySalonDataStore dataStore) : base(dataStore.Services) { }
}