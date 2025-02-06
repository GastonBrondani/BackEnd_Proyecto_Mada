

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
            
                return await _context.productos.ToListAsync();
            
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<producto>> GetProductoPorID(int id)
        {
            
                return await _context.productos.FindAsync(id);
            
            
        }

        [HttpPost]
        public async Task<ActionResult<producto>> PostProducto(producto producto)
        {
            
                _context.productos.Add(producto);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(PostProducto), new { id = producto.id_producto }, producto);
            
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, producto producto)
        {
            
                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<producto>> DeleteProducto(int id)
        {
            
                _context.productos.Remove(await _context.productos.FindAsync(id));
                await _context.SaveChangesAsync();
                return NoContent();
            
        }
        
    }
}
