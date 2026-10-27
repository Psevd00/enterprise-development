using BeautySalon.Domain;
using BeautySalon.Infrastructure.InMemory;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(ClientRepository clientRepository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Client>> GetAll() =>
        Ok(clientRepository.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<Client> GetById(int id)
    {
        var client = clientRepository.GetById(id);
        if (client is null)
        {
            return NotFound(new { message = "Клиент не найден" });
        }
        return Ok(client);
    }

    [HttpPost]
    public ActionResult<Client> Create([FromBody] Client client)
    {
        if (client is null)
        {
            return BadRequest("Некорректные данные клиента.");
        }

        var created = clientRepository.Add(client);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Client client)
    {
        try
        {
            clientRepository.Update(client, id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Клиент не найден" });
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var success = clientRepository.Delete(id);
        if (!success)
        {
            return NotFound(new { message = "Клиент не найден" });
        }
        return NoContent();
    }
}