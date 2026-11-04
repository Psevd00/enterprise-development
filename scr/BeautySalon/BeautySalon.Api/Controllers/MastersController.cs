using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Контроллер для управления мастерами
/// </summary>
public class MastersController(IMasterService service, ILogger<MastersController> logger)
    : CrudControllerBase<MasterDto, MasterEditDto>(service, logger);