using BeautySalon.Contracts.Dto;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Сервис управления записями на приём
/// </summary>
public interface IAppointmentService : ICrudService<AppointmentDto, AppointmentEditDto>;