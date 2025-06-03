using BackendMada.DTOs.Reporte;

namespace BackendMada.Service.Interfaces
{
    public interface IReporteService
    {
        Task<ProductoMasVendidoDTO> ObtenerProductoMasVendidoAsync();
        Task<List<GananciaPorFechaDTO>> ObtenerGananciaEntreFechasAsync(DateTime desde, DateTime hasta);
        Task<List<VentaPorClienteDTO>> ObtenerVentasPorClienteAsync();
        Task<List<ProductoStockBajoDTO>> ObtenerProductosConStockBajoAsync(int umbral = 7);
        Task<List<ProductoSinVentaDTO>> ObtenerProductosSinVentaDesdeAsync(DateTime desde);
        Task<List<EvolucionVentasDTO>> ObtenerEvolucionVentasAsync(string tipo); // tipo = "diaria", "semanal", "mensual"
        Task<List<GananciaPorProductoDTO>> ObtenerGananciaPorProductoAsync();
    }
}