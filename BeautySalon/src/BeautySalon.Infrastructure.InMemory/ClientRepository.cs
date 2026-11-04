using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Репозиторий клиентов
/// </summary>
public class ClientRepository(BeautySalonDataStore dataStore)
    : InMemoryRepository<Client>(dataStore.Clients), IClientRepository;