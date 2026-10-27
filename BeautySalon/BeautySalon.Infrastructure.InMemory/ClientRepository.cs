using BeautySalon.Domain;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Репозиторий для управления данными клиентов
/// </summary>
public class ClientRepository : InMemoryRepository<Client>
{
    public ClientRepository(BeautySalonDataStore dataStore) : base(dataStore.Clients) { }
}