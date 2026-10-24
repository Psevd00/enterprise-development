namespace BeautySalon.Infrastructure.InMemory;

using BeautySalon.Domain;

public class ClientRepository : InMemoryRepository<Client>
{
    public ClientRepository(BeautySalonDataStore dataStore) : base(dataStore.Clients) { }
}