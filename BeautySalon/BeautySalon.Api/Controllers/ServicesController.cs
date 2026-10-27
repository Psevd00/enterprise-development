using BeautySalon.Domain;
using BeautySalon.Infrastructure.InMemory;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Контроллер для управления услугами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ServicesController(ServiceRepository serviceRepository) : ControllerBase
{
    /// <summary>
    /// Получает список всех услуг
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Service>> GetAll() =>
        Ok(serviceRepository.GetAll());

    /// <summary>
    /// Получает услугу по идентификатору
    /// </summary>
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

    /// <summary>
    /// Добавляет новую услугу
    /// </summary>
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

    /// <summary>
    /// Обновляет существующую услугу по идентификатору
    /// </summary>
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

    /// <summary>
    /// Удаляет услугу по идентификатору
    /// <summary>
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