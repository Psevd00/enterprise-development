using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Репозиторий мастеров
/// </summary>
public class MasterRepository(BeautySalonDataStore dataStore)
    : InMemoryRepository<Master>(dataStore.Masters), IMasterRepository;