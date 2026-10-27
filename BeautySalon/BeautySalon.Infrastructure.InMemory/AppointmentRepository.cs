using BeautySalon.Domain;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Определяет операции доступа к данным записям
/// </summary>
public class AppointmentRepository : InMemoryRepository<Appointment>
{
    public AppointmentRepository(BeautySalonDataStore dataStore) : base(dataStore.Appointments) { }
}