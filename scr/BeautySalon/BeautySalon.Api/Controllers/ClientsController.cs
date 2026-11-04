using BeautySalon.Domain;
using BeautySalon.Infrastructure.InMemory;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Контроллер для управления клиентами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(ClientRepository clientRepository) : ControllerBase
{
    /// <summary>
    /// Получает список всех клиентов
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Client>> GetAll() =>
        Ok(clientRepository.GetAll());

    /// <summary>
    /// Получает клиента по идентификатору
    /// </summary>
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
    
    /// <summary>
    /// Добавляет нового клиента
    /// </summary>
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

    /// <summary>
    /// Обновляет информацию о клиенте по идентификатору
    /// </summary>
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

    /// <summary>
    /// Удаляет клиента по идентификатору
    /// </summary>
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