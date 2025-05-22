

using BackendMada.Data;
using BackendMada.Models;
using BackendMada.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProductoPorID(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null) return NotFound("Producto no encontrado");
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto([FromBody] Producto producto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var creado = await _productoService.CreateAsync(producto);
            return CreatedAtAction(nameof(GetProductoPorID), new { id = creado.id_producto }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, [FromBody] Producto producto)
        {
            if (id != producto.id_producto) return BadRequest("ID no coincide");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var actualizado = await _productoService.UpdateAsync(id, producto);
            if (!actualizado) return NotFound("Producto no encontrado");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var eliminado = await _productoService.DeleteAsync(id);
            if (!eliminado) return NotFound("Producto no encontrado");
            return NoContent();
        }

        [HttpGet("stockConProveedor")]
        public async Task<IActionResult> GetStockConProveedor([FromQuery] string nombreProducto)
        {
            try
            {
                var resultado = await _productoService.GetStockConProveedor(nombreProducto);
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