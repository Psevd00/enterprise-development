using BeautySalon.Application.Services;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Domain.Entities;
using BeautySalon.Infrastructure.InMemory;
using System.Diagnostics.Metrics;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace BeautySalon.Tests;

/// <summary>
/// Фикстура с тестовыми данными из in-memory хранилища и сервисом аналитики поверх репозиториев
/// </summary>
public class BeautySalonFixture
{
    private readonly BeautySalonDataStore _dataStore = new();

    /// <summary>
    /// Клиенты салона
    /// </summary>
    public List<Client> Clients => _dataStore.Clients;

    /// <summary>
    /// Мастера салона
    /// </summary>
    public List<Master> Masters => _dataStore.Masters;

    /// <summary>
    /// Услуги салона
    /// </summary>
    public List<Service> Services => _dataStore.Services;

    /// <summary>
    /// Записи на приём
    /// </summary>
    public List<Appointment> Appointments => _dataStore.Appointments;

    /// <summary>
    /// Сервис аналитики, работающий с теми же данными через репозитории
    /// </summary>
    public IAnalyticsService Analytics { get; }

    /// <summary>
    /// Создаёт фикстуру на данных из сида и собирает сервис аналитики
    /// </summary>
    public BeautySalonFixture()
    {
        Analytics = new AnalyticsService(
            new MasterRepository(_dataStore),
            new AppointmentRepository(_dataStore));
    }
}