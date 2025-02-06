using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendMada.Data;
using BackendMada.Models;

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
        //vamos a controlar las excepciones en una capa middleware
        [HttpGet]
        public async Task<ActionResult<IEnumerable<cliente>>> GetClientes()
        {
                return await _context.clientes.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<cliente>> GetClientePorID(int id)
        {
            var cliente = await _context.clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return await _context.clientes.FindAsync(id);
        }
        
        [HttpPost]
        public async Task<ActionResult<cliente>> PostCliente(cliente cliente)
        {
            _context.clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostCliente), new { id = cliente.id_cliente }, cliente);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, cliente cliente)
        {
            if (id != cliente.id_cliente)
            {
                return BadRequest();
            }
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
    }
}

