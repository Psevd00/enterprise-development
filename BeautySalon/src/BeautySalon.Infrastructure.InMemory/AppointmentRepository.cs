using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Репозиторий записей на приём
/// </summary>
public class AppointmentRepository(BeautySalonDataStore dataStore)
    : InMemoryRepository<Appointment>(dataStore.Appointments), IAppointmentRepository;