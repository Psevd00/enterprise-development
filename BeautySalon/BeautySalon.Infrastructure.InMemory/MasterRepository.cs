namespace BeautySalon.Infrastructure.InMemory;

using BeautySalon.Domain;

public class MasterRepository : InMemoryRepository<Master>
{
    public MasterRepository(BeautySalonDataStore dataStore) : base(dataStore.Masters) { }
}