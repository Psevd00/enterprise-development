using BeautySalon.Domain;

namespace BeautySalon.Tests;


public class SalonTests(BeautySalonFixture fixture) : IClassFixture<BeautySalonFixture>
{
   

    /// <summary>
    /// Вывести информацию о всех мастерах, стаж работы которых не менее 5 лет
    /// </summary>
    [Fact]
    public void GetMastesWithExperienceAtLeast5Years_ReturnCorrectMasters()
    {
        var masters = _fixture.Master
            .Where(m => m.WorkExperience >= 5)
            .ToList();

        Assert.NotEmpty(masters);
        Assert.All(masters, m => Assert.True(m.WorkExperience >= 5));
    }

    /// <summary>
    /// Вывести информацию о всех окошках 💅 выбранного мастера
    /// </summary>
    [Fact]
    public void GetFreeTimeSlotsForMasterOnDate_ReturnsWindows()
    {
        var targetMaster = _fixture.Master[0];
        var targetDate = new DateTime(2024, 6, 1);

        var workStart = new DateTime(2024, 6, 1, 9, 0, 0);
        var workEnd = new DateTime(2024, 6, 1, 18, 0, 0);

        var busySlots = _fixture.Appointment
            .Where(a => a.Master == targetMaster && a.DateTime.Date == targetDate.Date)
            .OrderBy(a => a.DateTime)
            .Select(a => new { Start = a.DateTime, End = a.DateTime + a.Service.Duration })
            .ToList();

        var freeSlots = new List<(DateTime Start, DateTime End)>();
        var currentPointer = workStart;

        foreach (var slot in busySlots)
        {
            if (slot.Start > currentPointer)
            {
                freeSlots.Add((currentPointer, slot.Start));
            }
            currentPointer = slot.End;
        }
        if (currentPointer < workEnd)
        {
            freeSlots.Add((currentPointer, workEnd));
        }
        Assert.NotEmpty(freeSlots);
        Assert.True(freeSlots.Count >= 2);
    }

    /// <summary>
    /// Вывести топ 5 наиболее популярных услуг
    /// </summary>
    [Fact]
    public void GetTop5PopularServices_ReturnsTop5Services()
    {
        var topServices = _fixture.Appointment
            .GroupBy(a => a.Service.Name)
            .Select(g => new
            {
                ServiceName = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(s => s.Count)
            .Take(5)
            .ToList();

        Assert.NotEmpty(topServices);
        Assert.True(topServices.Count <= 5);
    }

    /// <summary>
    /// Вывести информацию о количестве повторных записей клиентов за последний месяц
    /// </summary>
    [Fact]
    public void GetClientsWithRepeatVisitsInMonth_ReturnsClients()
    {
        int targetYear = 2024;
        int targetMonth = 6;

        var repeatClients = _fixture.Appointment
            .Where(a => a.DateTime.Year == targetYear && a.DateTime.Month == targetMonth)
            .GroupBy(a => a.Client)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        Assert.Single(repeatClients); // В нашей фикстуре это Иванова Анна Ивановна (Client[0])
        Assert.Equal("Иванова Анна Ивановна", repeatClients[0].FullName);
    }

    /// <summary>
    /// Вывести информацию о клиентах, записанных к нескольким мастерам, упорядочить по дате рождения
    /// </summary>
    [Fact]
    public void GetClientsWhoVisitedMultipleMasters_ReturnsClients()
    {
        var clients = _fixture.Appointment
            .GroupBy(a => a.Client)
            .Where(g => g.Select(a => a.Master).Distinct().Count() >= 2)
            .Select(g => g.Key)
            .OrderBy(c => c.BirthDate)
            .ToList();

        Assert.Single(clients);
        Assert.Equal("Иванова Анна Ивановна", clients[0].FullName);
    }
}