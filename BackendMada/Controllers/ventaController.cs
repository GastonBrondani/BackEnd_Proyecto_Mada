using BackendMada.Data;
using BackendMada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly MyDbContext _context;
        
        public VentaController(MyDbContext context)
        {
            _context = context;
        }
        //vamos a controlar las excepciones en una capa middleware
        [HttpGet]
        public async Task<ActionResult<IEnumerable<venta>>> GetVentas()
        {
                return await _context.venta.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<venta>> GetVentaPorID(int id)
        {
            var venta = await _context.venta.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            return await _context.venta.FindAsync(id);
        }
        
        [HttpPost]
        public async Task<ActionResult<venta>> PostVenta(venta venta)
        {
            _context.venta.Add(venta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostVenta), new { id = venta.id_venta }, venta);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenta(int id, venta venta)
        {
            if (id != venta.id_venta)
            {
                return BadRequest();
            }
            _context.Entry(venta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            var venta = await _context.venta.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            _context.venta.Remove(venta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

