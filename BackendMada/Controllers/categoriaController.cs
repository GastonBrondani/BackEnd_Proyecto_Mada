using BackendMada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendMada.Data;
using BackendMada.Service.Interfaces;

namespace BackendMada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        //vamos a controlar las excepciones en una capa middleware
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoriaPorID(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null) return NotFound("Categoria no encontrada");
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var creado = await _categoriaService.CreateAsync(categoria);
            return CreatedAtAction(nameof(GetCategoriaPorID), new { id = creado.id_categoria }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.id_categoria) return BadRequest("ID no coincide");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var actualizado = await _categoriaService.UpdateAsync(id, categoria);
            if (!actualizado) return NotFound("Categoria no encontrada");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var eliminado = await _categoriaService.DeleteAsync(id);
            if (!eliminado) return NotFound("Categoria no encontrada");
            return NoContent();
        }

        [HttpGet("nombre/{nombre}")]
        public async Task<ActionResult<IEnumerable<Categoria>>> ObtenerPorNombre(string nombre)
        {
            try
            {
                var resultado = await _categoriaService.ObtenerPorNombre(nombre);
                return Ok(resultado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

