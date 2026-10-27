using BeautySalon.Domain;
using BeautySalon.Infrastructure.InMemory;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Контроллер для управления мастерами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MastersController(MasterRepository masterRepository) : ControllerBase
{
    /// <summary>
    /// Получает список всех мастеров
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Master>> GetAll() =>
        Ok(masterRepository.GetAll());

    /// <summary>
    /// Получает мастера по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<Master> GetById(int id)
    {
        var master = masterRepository.GetById(id);
        if (master is null)
        {
            return NotFound(new { message = "Мастер не найден" });
        }
        return Ok(master);
    }

    /// <summary>
    /// Добавляет нового мастера
    /// </summary>
    [HttpPost]
    public ActionResult<Master> Create([FromBody] Master master)
    {
        if (master is null)
        {
            return BadRequest("Некорректные данные мастера.");
        }

        var created = masterRepository.Add(master);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновляет информацию о мастере
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Master master)
    {
        try
        {
            masterRepository.Update(master, id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Мастер не найден" });
        }
    }

    /// <summary>
    /// Удаляет мастера по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var success = masterRepository.Delete(id);
        if (!success)
        {
            return NotFound(new { message = "Мастер не найден" });
        }
        return NoContent();
    }
}