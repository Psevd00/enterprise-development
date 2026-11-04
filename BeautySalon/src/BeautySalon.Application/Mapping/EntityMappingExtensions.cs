using BeautySalon.Contracts.Dto;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Application.Mapping;

/// <summary>
/// Маппинг доменных сущностей в DTO для чтения
/// </summary>
public static class EntityMappingExtensions
{
    /// <summary>
    /// Преобразует клиента в DTO
    /// </summary>
    public static ClientDto ToDto(this Client client) => new()
    {
        Id = client.Id,
        FullName = client.FullName,
        Gender = client.Gender,
        BirthDate = client.BirthDate,
        PhoneNumber = client.PhoneNumber
    };

    /// <summary>
    /// Преобразует мастера в DTO
    /// </summary>
    public static MasterDto ToDto(this Master master) => new()
    {
        Id = master.Id,
        FullName = master.FullName,
        Gender = master.Gender,
        PassportNumber = master.PassportNumber,
        Specialization = master.Specialization,
        WorkExperience = master.WorkExperience
    };

    /// <summary>
    /// Преобразует услугу в DTO
    /// </summary>
    public static ServiceDto ToDto(this Service service) => new()
    {
        Id = service.Id,
        Name = service.Name,
        Category = service.Category,
        Price = service.Price,
        Duration = service.Duration
    };

    /// <summary>
    /// Преобразует запись на приём в DTO
    /// </summary>
    public static AppointmentDto ToDto(this Appointment appointment) => new()
    {
        Id = appointment.Id,
        ClientId = appointment.Client.Id,
        ClientFullName = appointment.Client.FullName,
        MasterId = appointment.Master.Id,
        MasterFullName = appointment.Master.FullName,
        ServiceId = appointment.Service.Id,
        ServiceName = appointment.Service.Name,
        DateTime = appointment.DateTime,
        IsRegularClient = appointment.IsRegularClient
    };
}
