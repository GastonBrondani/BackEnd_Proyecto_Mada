
using BackendMada.Data;
using BackendMada.Models;
using BackendMada.Utils; // ✅ Asegurate de tener este using
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ClienteController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<cliente>>> GetClientes()
        {
            return await _context.clientes.ToListAsync();
        }

        // GET: api/cliente/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<cliente>> GetClientePorID(int id)
        {
            var cliente = await _context.clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            return cliente;
        }

        // POST: api/cliente
        [HttpPost]
        public async Task<ActionResult<cliente>> PostCliente(cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!ValidadorCUIT.EsCuitValido(cliente.cuit_cuil))
                return BadRequest("CUIT inválido: formato incorrecto o dígito verificador inválido.");

            _context.clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostCliente), new { id = cliente.id_cliente }, cliente);
        }

        // PUT: api/cliente/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, cliente cliente)
        {
            if (id != cliente.id_cliente)
                return BadRequest("El ID de la URL no coincide con el del objeto enviado.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!ValidadorCUIT.EsCuitValido(cliente.cuit_cuil))
                return BadRequest("CUIT inválido: formato incorrecto o dígito verificador inválido.");

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/cliente/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
