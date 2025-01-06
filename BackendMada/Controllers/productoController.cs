

using BackendMada.Data;
using BackendMada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProductoController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<producto>>> GetProductos()
        {
            try
            {
                return Ok(await _context.productos.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<producto>> GetProductoPorID(int id)
        {
            try
            {
                return Ok(await _context.productos.FindAsync(id));
            }
            catch (Exception e)
            {
                return StatusCode(404,$"Error al obtener el producto: {e.Message}");
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<producto>> PostProducto(producto producto)
        {
            try
            {
                _context.productos.Add(producto);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(PostProducto), new { id = producto.id_producto }, producto);
            }
            catch (Exception e)
            {
                return StatusCode(409, $"Error al crear el producto: {e.Message}");
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, producto producto)
        {
            try
            {
                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(409, $"Error al modificar el producto: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<producto>> DeleteProducto(int id)
        {
            try
            {
                
                _context.productos.Remove(await _context.productos.FindAsync(id));
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(409, $"Error al eliminar el producto: {e.Message}");
            }
        }
        
    }
}
