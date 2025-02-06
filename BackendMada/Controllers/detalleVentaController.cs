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
        public async Task<ActionResult<IEnumerable<datalle_venta>>> GetDetalleVentas()
        {
                return await _context.datalle_venta.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<datalle_venta>> GetDetalleVentaPorID(int id)
        {
            var detalleVenta = await _context.datalle_venta.FindAsync(id);
            if (detalleVenta == null)
            {
                return NotFound();
            }
            return await _context.datalle_venta.FindAsync(id);
        }
        
        [HttpPost]
        public async Task<ActionResult<datalle_venta>> PostDetalleVenta(datalle_venta detalleVenta)
        {
            _context.datalle_venta.Add(detalleVenta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostDetalleVenta), new { id = detalleVenta.id_detalle }, detalleVenta);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleVenta(int id, datalle_venta detalleVenta)
        {
            if (id != detalleVenta.id_detalle)
            {
                return BadRequest();
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

