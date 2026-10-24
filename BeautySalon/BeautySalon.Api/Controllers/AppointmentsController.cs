namespace BeautySalon.Api.Controllers;

using BeautySalon.Domain;
using BeautySalon.Infrastructure.InMemory;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(AppointmentRepository appointmentRepository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Appointment>> GetAll() =>
        Ok(appointmentRepository.GetAll());

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