using BeautySalon.Domain;
using BeautySalon.Infrastructure.InMemory;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ServicesController(ServiceRepository serviceRepository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Service>> GetAll() =>
        Ok(serviceRepository.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<Service> GetById(int id)
    {
        var service = serviceRepository.GetById(id);
        if (service is null)
        {
            return NotFound(new { message = "Услуга не найдена" });
        }
        return Ok(service);
    }

    [HttpPost]
    public ActionResult<Service> Create([FromBody] Service service)
    {
        if (service is null)
        {
            return BadRequest("Некорректные данные услуги.");
        }

        var created = serviceRepository.Add(service);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Service service)
    {
        try
        {
            serviceRepository.Update(service, id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Услуга не найдена" });
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var success = serviceRepository.Delete(id);
        if (!success)
        {
            return NotFound(new { message = "Услуга не найдена" });
        }
        return NoContent();
    }
}