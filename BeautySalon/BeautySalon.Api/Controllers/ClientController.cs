namespace BeautySalon.Api.Controllers;

using BeautySalon.Domain;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClientController(BeautySalonDataStore dataStore) : ControllerBase
{
    // GET: api/client
    [HttpGet]
    public ActionResult<IEnumerable<Client>> GetAll()
    {
        return Ok(dataStore.Clients);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Client> GetById(int id)
    {
        if (id < 0 || id >= dataStore.Clients.Count)
        {
            return NotFound(new { message = "Клиент не найден" });
        }

        return Ok(dataStore.Clients[id]);
    }

    [HttpPost]
    public ActionResult<Client> Create([FromBody] Client newClient)
    {
        if (newClient == null)
        {
            return BadRequest("Некорректные данные клиента.");
        }

        dataStore.Clients.Add(newClient);
        return CreatedAtAction(nameof(GetById), new { id = dataStore.Clients.Count - 1 }, newClient);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (id < 0 || id >= dataStore.Clients.Count)
        {
            return NotFound(new { message = "Клиент не найден" });
        }

        dataStore.Clients.RemoveAt(id);
        return NoContent();
    }
}