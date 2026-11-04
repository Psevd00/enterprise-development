namespace BeautySalon.Api.Controllers;

using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;


/// <summary>
/// Контроллер для управления записями клиентов на приём
/// </summary>
public class AppointmentsController(IAppointmentService service, ILogger<AppointmentsController> logger)
    : CrudControllerBase<AppointmentDto, AppointmentEditDto>(service, logger);