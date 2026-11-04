using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Контроллер для управления клиентами
/// </summary>
public class ClientsController(IClientService service, ILogger<ClientsController> logger)
    : CrudControllerBase<ClientDto, ClientEditDto>(service, logger);