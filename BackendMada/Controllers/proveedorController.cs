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
            try
            {
                return Ok(await _context.proveedores.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<proveedor>> GetProveedorPorID(int id)
        {
            try
            {
                return Ok(await _context.proveedores.FindAsync(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<proveedor>> PostProveedor(proveedor proveedor)
        {
            try
            {
                _context.proveedores.Add(proveedor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(PostProveedor), new { id = proveedor.id_proveedor }, proveedor);
            }
            catch (Exception e)
            {
                return StatusCode(409,$"No se pudo crear el proveedor: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, proveedor proveedor)
        {
            try
            {
                _context.Entry(proveedor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(409,$"No se pudo actualizar el proveedor: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<proveedor>> DeleteProveedor(int id)
        {
            try
            {
                _context.proveedores.Remove(await _context.proveedores.FindAsync(id));
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(409,$"El proveedor no se pudo eliminar: {e.Message}");
            }
        }
    }
}

