namespace BeautySalon.Infrastructure.InMemory;

using BeautySalon.Domain;

public class ServiceRepository : InMemoryRepository<Service>
{
    public ServiceRepository(BeautySalonDataStore dataStore) : base(dataStore.Services) { }
}