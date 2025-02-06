using BackendMada.Data;
using BackendMada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProveedorController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<proveedor>>> GetProveedores()
        {
            return await _context.proveedores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<proveedor>> GetProveedorPorID(int id)
        {
            var cliente = await _context.clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return await _context.proveedores.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<proveedor>> PostProveedor(proveedor proveedor)
        {
                _context.proveedores.Add(proveedor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(PostProveedor), new { id = proveedor.id_proveedor }, proveedor);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, proveedor proveedor)
        {
            
                _context.Entry(proveedor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<proveedor>> DeleteProveedor(int id)
        {
                _context.proveedores.Remove(await _context.proveedores.FindAsync(id));
                await _context.SaveChangesAsync();
                return NoContent();
            
        }
    }
}

