namespace BeautySalon.Api.Controllers;

using BeautySalon.Domain;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MastersController(BeautySalonDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Master>> GetAll()
    {
        return Ok(dataStore.Masters);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Master> GetById(int id)
    {
        if (id < 0 || id >= dataStore.Masters.Count)
        {
            return NotFound(new { message = "Мастер не найден" });
        }

        return Ok(dataStore.Masters[id]);
    }

    [HttpPost]
    public ActionResult<Master> Create([FromBody] Master newMaster)
    {
        if (newMaster == null)
        {
            return BadRequest("Некорректные данные мастера.");
        }

        dataStore.Masters.Add(newMaster);
        return CreatedAtAction(nameof(GetById), new { id = dataStore.Masters.Count - 1 }, newMaster);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (id < 0 || id >= dataStore.Masters.Count)
        {
            return NotFound(new { message = "Мастер не найден" });
        }

        dataStore.Masters.RemoveAt(id);
        return NoContent();
    }
}