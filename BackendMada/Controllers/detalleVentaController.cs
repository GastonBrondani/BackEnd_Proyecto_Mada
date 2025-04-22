using BackendMada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendMada.Data;

namespace BackendMada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleVentaController : ControllerBase
    {
        private readonly MyDbContext _context;
        
        public DetalleVentaController(MyDbContext context)
        {
            _context = context;
        }
        //vamos a controlar las excepciones en una capa middleware
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalle_venta>>> GetDetalleVentas()
        {
                return await _context.datalle_venta.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalle_venta>> GetDetalleVentaPorID(int id)
        {
            var detalleVenta = await _context.datalle_venta.FindAsync(id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            return detalleVenta;
        }
        
        [HttpPost]
        public async Task<ActionResult<Detalle_venta>> PostDetalleVenta(Detalle_venta detalleVenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.datalle_venta.Add(detalleVenta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDetalleVentas), new { id = detalleVenta.id_detalle }, detalleVenta);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleVenta(int id, Detalle_venta detalleVenta)
        {
            if (id != detalleVenta.id_detalle)
            {
                return BadRequest("El ID no coincide.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Entry(detalleVenta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleVenta(int id)
        {
            var detalleVenta = await _context.datalle_venta.FindAsync(id);
            if (detalleVenta == null)
            {
                return NotFound();
            }
            _context.datalle_venta.Remove(detalleVenta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

