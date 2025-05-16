using BackendMada.Models;
using BackendMada.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendMada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes() =>
            Ok(await _clienteService.ObtenerTodosLosClientes());

        [HttpGet("apellido/{apellido}")]
        public async Task<ActionResult<IEnumerable<Cliente>>> FiltrarPorApellido(string apellido) =>
            Ok(await _clienteService.FiltrarPorApellido(apellido));

        [HttpGet("cuit/{cuit}")]
        public async Task<ActionResult<Cliente>> FiltrarPorCuit(string cuit)
        {
            var cliente = await _clienteService.FiltrarPorCuit(cuit);
            return cliente is null ? NotFound() : Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (exitoso, mensaje, nuevo) = await _clienteService.CrearCliente(cliente);
            if (!exitoso) return BadRequest(mensaje);

            return CreatedAtAction(nameof(FiltrarPorCuit), new { cuit = nuevo!.cuit_cuil }, nuevo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.id_cliente) return BadRequest("El ID no coincide.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var actualizado = await _clienteService.ActualizarCliente(cliente);
            return actualizado ? NoContent() : NotFound("Cliente no encontrado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var eliminado = await _clienteService.EliminarCliente(id);
            return eliminado ? NoContent() : NotFound("Cliente no encontrado.");
        }
    }
}
