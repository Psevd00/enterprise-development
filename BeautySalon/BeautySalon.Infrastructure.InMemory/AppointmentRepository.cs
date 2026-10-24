namespace BeautySalon.Infrastructure.InMemory;

using BeautySalon.Domain;

public class AppointmentRepository : InMemoryRepository<Appointment>
{
    public AppointmentRepository(BeautySalonDataStore dataStore) : base(dataStore.Appointments) { }
}