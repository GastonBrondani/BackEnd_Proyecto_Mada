
using BackendMada.Models;
using BackendMada.Service;
using BackendMada.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendMada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly iProveedorService _proveedorService;

        public ProveedorController(ProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
        {
            var proveedores = await _proveedorService.ObtenerTodos();
            return Ok(proveedores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedorPorID(int id)
        {
            var proveedor = await _proveedorService.ObtenerPorId(id);
            if (proveedor == null)
                return NotFound();
            return Ok(proveedor);
        }

        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_proveedorService.EsCuitValido(proveedor.cuil_proveedor))
                return BadRequest("CUIT inválido.");

            if (await _proveedorService.ExisteCuit(proveedor.cuil_proveedor))
                return BadRequest("El CUIT ya está registrado.");

            var nuevo = await _proveedorService.CrearProveedor(proveedor);
            return CreatedAtAction(nameof(GetProveedorPorID), new { id = nuevo.id_proveedor }, nuevo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.id_proveedor)
                return BadRequest("El ID no coincide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actualizado = await _proveedorService.ActualizarProveedor(proveedor);
            if (!actualizado)
                return NotFound("Proveedor no encontrado.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var eliminado = await _proveedorService.EliminarProveedor(id);
            if (!eliminado)
                return NotFound("Proveedor no encontrado.");

            return NoContent();
        }
    }
}
