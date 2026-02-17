using hanumachantsApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace hanumachantsApi.Controllers
{
    [ApiController]
    [Route("api/session")]
    public class SessionController : Controller
    {
        private readonly TableService _service;

        public SessionController(TableService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var session = await _service.CreateAsync();
            return Ok(session);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var session = await _service.GetAsync(id);
            if (session == null) return NotFound();
            return Ok(session);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Complete(string id, [FromBody] SessionUpdateDto dto)
        {
            Console.WriteLine($"PUT HIT FOR ID: {id}");

            if (dto == null)
                return BadRequest();

            var updated = await _service.UpdateAsync(id, dto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }
    }
}
