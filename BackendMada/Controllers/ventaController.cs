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
        public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
        {
                return await _context.venta.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetVentaPorID(int id)
        {
            var venta = await _context.venta.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            return venta;
        }
        
        [HttpPost]
        public async Task<ActionResult<Venta>> PostVenta(Venta venta)
        {
            if (!ModelState.IsValid)
            {
              return BadRequest(ModelState);
            }
            _context.venta.Add(venta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVentas), new { id = venta.id_venta }, venta);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenta(int id, Venta venta)
        {
            if (id != venta.id_venta)
            {
                return BadRequest("El ID no coincide.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

