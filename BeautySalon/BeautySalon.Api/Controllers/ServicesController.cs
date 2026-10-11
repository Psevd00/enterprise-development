namespace BeautySalon.Api.Controllers;

using BeautySalon.Domain;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ServicesController(BeautySalonDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Service>> GetAll()
    {
        return Ok(dataStore.Services);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Service> GetById(int id)
    {
        if (id < 0 || id >= dataStore.Services.Count)
        {
            return NotFound(new { message = "Услуга не найдена" });
        }

        return Ok(dataStore.Services[id]);
    }

    [HttpPost]
    public ActionResult<Service> Create([FromBody] Service newService)
    {
        if (newService == null)
        {
            return BadRequest("Некорректные данные услуги.");
        }

        dataStore.Services.Add(newService);
        return CreatedAtAction(nameof(GetById), new { id = dataStore.Services.Count - 1 }, newService);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (id < 0 || id >= dataStore.Services.Count)
        {
            return NotFound(new { message = "Услуга не найдена" });
        }

        dataStore.Services.RemoveAt(id);
        return NoContent();
    }
}