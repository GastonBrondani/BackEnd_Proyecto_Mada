using BackendMada.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendMada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReporteController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("producto-mas-vendido")]
        public async Task<IActionResult> GetProductoMasVendido()
        {
            var resultado = await _reporteService.ObtenerProductoMasVendidoAsync();
            return Ok(resultado);
        }

        [HttpGet("ganancia-entre-fechas")]
        public async Task<IActionResult> GetGananciaEntreFechas([FromQuery] DateTime desde, [FromQuery] DateTime hasta)
        {
            var resultado = await _reporteService.ObtenerGananciaEntreFechasAsync(desde, hasta);
            return Ok(resultado);
        }

        [HttpGet("ventas-por-cliente")]
        public async Task<IActionResult> GetVentasPorCliente()
        {
            var resultado = await _reporteService.ObtenerVentasPorClienteAsync();
            return Ok(resultado);
        }

        [HttpGet("productos-con-stock-bajo")]
        public async Task<IActionResult> GetProductosConStockBajo([FromQuery] int umbral = 7)
        {
            var resultado = await _reporteService.ObtenerProductosConStockBajoAsync(umbral);
            return Ok(resultado);
        }

        [HttpGet("productos-sin-venta-desde")]
        public async Task<IActionResult> GetProductosSinVentaDesde([FromQuery] DateTime desde)
        {
            var resultado = await _reporteService.ObtenerProductosSinVentaDesdeAsync(desde);
            return Ok(resultado);
        }

        [HttpGet("evolucion-ventas")]
        public async Task<IActionResult> GetEvolucionVentas([FromQuery] string tipo = "diaria")
        {
            var resultado = await _reporteService.ObtenerEvolucionVentasAsync(tipo);
            return Ok(resultado);
        }

        [HttpGet("ganancia-por-producto")]
        public async Task<IActionResult> GetGananciaPorProducto()
        {
            var resultado = await _reporteService.ObtenerGananciaPorProductoAsync();
            return Ok(resultado);
        }
    }
}
