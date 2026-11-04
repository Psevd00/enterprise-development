namespace BeautySalon.Api.Controllers;

using System.ComponentModel.DataAnnotations;
using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;


/// <summary>
/// Аналитические запросы по данным салона красоты
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Мастера со стажем не менее указанного
    /// </summary>
    [HttpGet("experienced-masters")]
    [ProducesResponseType<IReadOnlyList<MasterDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<MasterDto>>> GetExperiencedMasters(
        [FromQuery, Range(0, 80)] int minExperience = 5,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return Ok(await service.GetExperiencedMastersAsync(minExperience, cancellationToken));
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            return Failure(ex, nameof(GetExperiencedMasters));
        }
    }

    /// <summary>
    /// Свободные окна мастера на выбранную дату (рабочий день 09:00–18:00)
    /// </summary>
    [HttpGet("masters/{masterId:int}/free-slots")]
    [ProducesResponseType<IReadOnlyList<TimeSlotDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<TimeSlotDto>>> GetFreeSlots(
        int masterId,
        [FromQuery, Required] DateOnly? date,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var slots = await service.GetFreeSlotsAsync(masterId, date!.Value, cancellationToken);
            if (slots is null)
            {
                logger.LogInformation("Мастер с идентификатором {MasterId} не найден", masterId);
                return Problem(statusCode: StatusCodes.Status404NotFound, title: "Мастер не найден");
            }

            return Ok(slots);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            return Failure(ex, nameof(GetFreeSlots));
        }
    }

    /// <summary>
    /// Топ наиболее популярных услуг по количеству записей
    /// </summary>
    [HttpGet("popular-services")]
    [ProducesResponseType<IReadOnlyList<PopularServiceDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<PopularServiceDto>>> GetPopularServices(
        [FromQuery, Range(1, 100)] int top = 5,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return Ok(await service.GetPopularServicesAsync(top, cancellationToken));
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            return Failure(ex, nameof(GetPopularServices));
        }
    }

    /// <summary>
    /// Клиенты, записавшиеся более одного раза в выбранном месяце
    /// </summary>
    [HttpGet("repeat-clients")]
    [ProducesResponseType<IReadOnlyList<RepeatClientDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<RepeatClientDto>>> GetRepeatClients(
        [FromQuery, Range(1, 9999)] int year,
        [FromQuery, Range(1, 12)] int month,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return Ok(await service.GetRepeatClientsAsync(year, month, cancellationToken));
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            return Failure(ex, nameof(GetRepeatClients));
        }
    }

    /// <summary>
    /// Клиенты, записанные к нескольким мастерам, упорядоченные по дате рождения
    /// </summary>
    [HttpGet("clients-with-multiple-masters")]
    [ProducesResponseType<IReadOnlyList<ClientDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetClientsWithMultipleMasters(
        CancellationToken cancellationToken = default)
    {
        try
        {
            return Ok(await service.GetClientsWithMultipleMastersAsync(cancellationToken));
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            return Failure(ex, nameof(GetClientsWithMultipleMasters));
        }
    }

    private ObjectResult Failure(Exception ex, string operation)
    {
        logger.LogError(ex, "Операция {Operation} завершилась ошибкой", operation);
        return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "Внутренняя ошибка сервера");
    }
}