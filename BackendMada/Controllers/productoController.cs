

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
        private readonly myDbContext _context;

        public ProductoController(myDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            
                return await _context.productos.ToListAsync();
            
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProductoPorID(int id)
        {
            
                return await _context.productos.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostProducto), new { id = producto.id_producto }, producto);
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.id_producto)
            {
                return BadRequest("El ID no coincide con el objeto enviado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

 

        [HttpDelete("{id}")]
        public async Task<ActionResult<Producto>> DeleteProducto(int id)
        {
            
                _context.productos.Remove(await _context.productos.FindAsync(id));
                await _context.SaveChangesAsync();
                return NoContent();
            
        }
        
    }
}
