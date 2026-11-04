using BeautySalon.Application.Mapping;
using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Application.Services;

/// <summary>
/// Аналитические запросы по данным салона красоты
/// </summary>
public class AnalyticsService(
    IMasterRepository masterRepository,
    IAppointmentRepository appointmentRepository) : IAnalyticsService
{
    private static readonly TimeOnly WorkDayStart = new(9, 0);
    private static readonly TimeOnly WorkDayEnd = new(18, 0);

    public async Task<IReadOnlyList<MasterDto>> GetExperiencedMastersAsync(
        int minExperienceYears = 5, CancellationToken cancellationToken = default)
    {
        var masters = await masterRepository.GetAllAsync(cancellationToken);

        return masters
            .Where(m => m.WorkExperience >= minExperienceYears)
            .Select(m => m.ToDto())
            .ToList();
    }

    public async Task<IReadOnlyList<TimeSlotDto>?> GetFreeSlotsAsync(
        int masterId, DateOnly date, CancellationToken cancellationToken = default)
    {
        var master = await masterRepository.GetByIdAsync(masterId, cancellationToken);
        if (master is null)
        {
            return null;
        }

        var dayStart = date.ToDateTime(WorkDayStart);
        var dayEnd = date.ToDateTime(WorkDayEnd);

        var appointments = await appointmentRepository.GetAllAsync(cancellationToken);
        var busySlots = appointments
            .Where(a => a.Master.Id == masterId && DateOnly.FromDateTime(a.DateTime) == date)
            .OrderBy(a => a.DateTime)
            .Select(a => (Start: a.DateTime, End: a.DateTime + a.Service.Duration))
            .ToList();

        var freeSlots = new List<TimeSlotDto>();
        var pointer = dayStart;

        foreach (var (start, end) in busySlots)
        {
            if (start >= dayEnd)
            {
                break;
            }

            if (start > pointer)
            {
                freeSlots.Add(new TimeSlotDto { Start = pointer, End = start });
            }

            if (end > pointer)
            {
                pointer = end;
            }
        }

        if (pointer < dayEnd)
        {
            freeSlots.Add(new TimeSlotDto { Start = pointer, End = dayEnd });
        }

        return freeSlots;
    }

    public async Task<IReadOnlyList<PopularServiceDto>> GetPopularServicesAsync(
        int top = 5, CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentRepository.GetAllAsync(cancellationToken);

        return appointments
            .GroupBy(a => a.Service.Name)
            .Select(g => new PopularServiceDto { ServiceName = g.Key, AppointmentsCount = g.Count() })
            .OrderByDescending(s => s.AppointmentsCount)
            .Take(top)
            .ToList();
    }

    public async Task<IReadOnlyList<RepeatClientDto>> GetRepeatClientsAsync(
        int year, int month, CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentRepository.GetAllAsync(cancellationToken);

        return appointments
            .Where(a => a.DateTime.Year == year && a.DateTime.Month == month)
            .GroupBy(a => a.Client.Id)
            .Where(g => g.Count() > 1)
            .Select(g => new RepeatClientDto
            {
                Client = g.First().Client.ToDto(),
                AppointmentsCount = g.Count()
            })
            .OrderByDescending(r => r.AppointmentsCount)
            .ToList();
    }

    public async Task<IReadOnlyList<ClientDto>> GetClientsWithMultipleMastersAsync(
        CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentRepository.GetAllAsync(cancellationToken);

        return appointments
            .GroupBy(a => a.Client.Id)
            .Where(g => g.Select(a => a.Master.Id).Distinct().Count() >= 2)
            .Select(g => g.First().Client)
            .OrderBy(c => c.BirthDate)
            .Select(c => c.ToDto())
            .ToList();
    }
}