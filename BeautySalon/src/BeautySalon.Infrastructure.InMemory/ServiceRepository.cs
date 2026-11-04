using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Репозиторий услуг
/// </summary>
public class ServiceRepository(BeautySalonDataStore dataStore)
    : InMemoryRepository<Service>(dataStore.Services), IServiceRepository;