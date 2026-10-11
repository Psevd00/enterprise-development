namespace BeautySalon.Api.Controllers;

using BeautySalon.Domain;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(BeautySalonDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Appointment>> GetAll()
    {
        return Ok(dataStore.Appointments);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Appointment> GetById(int id)
    {
        if (id < 0 || id >= dataStore.Appointments.Count)
        {
            return NotFound(new { message = "Запись не найдена" });
        }

        return Ok(dataStore.Appointments[id]);
    }

    [HttpPost]
    public ActionResult<Appointment> Create([FromBody] Appointment newAppointment)
    {
        if (newAppointment == null)
        {
            return BadRequest("Некорректные данные записи.");
        }

        dataStore.Appointments.Add(newAppointment);
        return CreatedAtAction(nameof(GetById), new { id = dataStore.Appointments.Count - 1 }, newAppointment);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (id < 0 || id >= dataStore.Appointments.Count)
        {
            return NotFound(new { message = "Запись не найдена" });
        }

        dataStore.Appointments.RemoveAt(id);
        return NoContent();
    }
}