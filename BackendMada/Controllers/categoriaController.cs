using BackendMada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendMada.Data;

namespace BackendMada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly MyDbContext _context;
        
        public CategoriaController(MyDbContext context)
        {
            _context = context;
        }
        //vamos a controlar las excepciones en una capa middleware
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
                return await _context.categoria.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoriaPorID(int id)
        {
            var categoria = await _context.categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return await _context.categoria.FindAsync(id);
        }
        
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.categoria.Add(categoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostCategoria), new { id = categoria.id_categoria }, categoria);
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.id_categoria)
            {
                return BadRequest();
            }
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            _context.categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

