using BackendMada.Data;
using BackendMada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<datalle_venta>>> GetDetalles()
        {
            return await _context.datalle_venta.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<datalle_venta>> GetDetalle(int id)
        {
            var detalle = await _context.datalle_venta.FindAsync(id);
            if (detalle == null)
            {
                return NotFound();
            }

            return detalle;
        }

        [HttpPost]
        public async Task<ActionResult<datalle_venta>> PostDetalle(datalle_venta detalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.datalle_venta.Add(detalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetalle), new { id = detalle.id_detalle }, detalle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalle(int id, datalle_venta detalle)
        {
            if (id != detalle.id_detalle)
            {
                return BadRequest("El ID no coincide.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(detalle).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalle(int id)
        {
            var detalle = await _context.datalle_venta.FindAsync(id);
            if (detalle == null)
            {
                return NotFound();
            }

            _context.datalle_venta.Remove(detalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
