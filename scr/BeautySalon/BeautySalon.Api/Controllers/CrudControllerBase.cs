namespace BeautySalon.Api.Controllers;

using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Exceptions;
using BeautySalon.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Базовый контроллер с CRUD-операциями
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class CrudControllerBase<TDto, TEditDto>(
    ICrudService<TDto, TEditDto> service,
    ILogger logger)
    : ControllerBase
    where TDto : class, IHasId
    where TEditDto : class
{
    /// <summary>
    /// Получает список всех записей
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<TDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await service.GetAllAsync(cancellationToken));
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            return HandleException(ex, nameof(GetAll));
        }
    }

    /// <summary>
    /// Получает запись по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var dto = await service.GetByIdAsync(id, cancellationToken);
            return dto is null ? NotFoundResult(id) : Ok(dto);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            return HandleException(ex, nameof(GetById));
        }
    }

    /// <summary>
    /// Создаёт новую запись
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Create([FromBody] TEditDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var created = await service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            return HandleException(ex, nameof(Create));
        }
    }

    /// <summary>
    /// Обновляет запись
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, [FromBody] TEditDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await service.UpdateAsync(id, dto, cancellationToken);
            return updated ? NoContent() : NotFoundResult(id);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            return HandleException(ex, nameof(Update));
        }
    }

    /// <summary>
    /// Удаляет запись
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var deleted = await service.DeleteAsync(id, cancellationToken);
            return deleted ? NoContent() : NotFoundResult(id);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            return HandleException(ex, nameof(Delete));
        }
    }

    private ObjectResult NotFoundResult(int id)
    {
        logger.LogInformation("Запись с идентификатором {Id} не найдена", id);
        return Problem(statusCode: StatusCodes.Status404NotFound, title: "Запись не найдена");
    }

    private ObjectResult HandleException(Exception ex, string operation)
    {
        if (ex is ReferenceNotFoundException referenceEx)
        {
            logger.LogWarning(referenceEx, "Операция {Operation}: не найдена связанная сущность", operation);
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Некорректные данные",
                detail: referenceEx.Message);
        }

        logger.LogError(ex, "Операция {Operation} завершилась ошибкой", operation);
        return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "Внутренняя ошибка сервера");
    }
}
