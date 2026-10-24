namespace BeautySalon.Api.Controllers;

using BeautySalon.Domain;
using BeautySalon.Infrastructure.InMemory;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MastersController(MasterRepository masterRepository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Master>> GetAll() =>
        Ok(masterRepository.GetAll());

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