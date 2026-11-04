using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Контроллер для управления услугами
/// </summary>
public class ServicesController(IServiceService service, ILogger<ServicesController> logger)
    : CrudControllerBase<ServiceDto, ServiceEditDto>(service, logger);