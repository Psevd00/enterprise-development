using BeautySalon.Domain;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Определяет операции доступа к данным мастерам
/// </summary>
public class MasterRepository : InMemoryRepository<Master>
{
    public MasterRepository(BeautySalonDataStore dataStore) : base(dataStore.Masters) { }
}