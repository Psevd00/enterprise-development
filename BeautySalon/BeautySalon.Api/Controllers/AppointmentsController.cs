namespace BeautySalon.Api.Controllers;

using BeautySalon.Domain;
using BeautySalon.Infrastructure.InMemory;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для управления записями клиентов.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(AppointmentRepository appointmentRepository) : ControllerBase
{
    /// <summary>
    /// Получает список всех записей
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Appointment>> GetAll() =>
        Ok(appointmentRepository.GetAll());

    /// <summary>
    /// Получает запись по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<Appointment> GetById(int id)
    {
        var appointment = appointmentRepository.GetById(id);
        if (appointment is null)
        {
            return NotFound(new { message = "Запись не найдена" });
        }
        return Ok(appointment);
    }

    /// <summary>
    /// Добавляет новую запись
    /// </summary>
    [HttpPost]
    public ActionResult<Appointment> Create([FromBody] Appointment appointment)
    {
        if (appointment is null)
        {
            return BadRequest("Некорректные данные записи.");
        }

        var created = appointmentRepository.Add(appointment);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновляет информацию о записи
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Appointment appointment)
    {
        try
        {
            appointmentRepository.Update(appointment, id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Запись не найдена" });
        }
    }

    /// <summary>
    /// Удаляет запись по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var success = appointmentRepository.Delete(id);
        if (!success)
        {
            return NotFound(new { message = "Запись не найдена" });
        }
        return NoContent();
    }
}