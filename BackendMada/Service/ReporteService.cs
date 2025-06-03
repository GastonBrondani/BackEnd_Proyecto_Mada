using BackendMada.Data;
using BackendMada.DTOs.Reporte;
using BackendMada.Service.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BackendMada.Service
{
    public class ReporteService : IReporteService
    {
        private readonly MyDbContext _context;

        public ReporteService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<ProductoMasVendidoDTO> ObtenerProductoMasVendidoAsync()
        {
            var resultado = await _context.datalle_venta
                .GroupBy(d => d.id_producto)
                .Select(g => new
                {
                    id_producto = g.Key,
                    cantidad = g.Sum(x => x.cantidad)
                })
                .OrderByDescending(x => x.cantidad)
                .FirstOrDefaultAsync();

            if (resultado == null) throw new Exception("No hay ventas registradas.");

            var producto = await _context.productos.FindAsync(resultado.id_producto);
            if (producto == null) throw new Exception("Producto no encontrado.");

            return new ProductoMasVendidoDTO
            {
                id_producto = producto.id_producto,
                nombre_producto = producto.nombre_producto,
                cantidad_total_vendida = resultado.cantidad
            };
        }

        public async Task<List<GananciaPorFechaDTO>> ObtenerGananciaEntreFechasAsync(DateTime desde, DateTime hasta)
        {
            return await _context.venta
                .Where(v => v.fecha_venta >= desde && v.fecha_venta <= hasta)
                .GroupBy(v => v.fecha_venta.Date)
                .Select(g => new GananciaPorFechaDTO
                {
                    fecha = g.Key,
                    total_ganancia = g.Sum(v => v.total_venta)
                })
                .OrderBy(g => g.fecha)
                .ToListAsync();
        }

        public async Task<List<VentaPorClienteDTO>> ObtenerVentasPorClienteAsync()
        {
            return await _context.venta
                .GroupBy(v => v.id_cliente)
                .Select(g => new VentaPorClienteDTO
                {
                    id_cliente = g.Key,
                    total_ventas = g.Count(),
                    total_monto = g.Sum(v => v.total_venta)
                })
                .ToListAsync();
        }

        public async Task<List<ProductoStockBajoDTO>> ObtenerProductosConStockBajoAsync(int umbral = 7)
        {
            return await _context.productos
                .Where(p => p.stock_producto <= umbral)
                .Select(p => new ProductoStockBajoDTO
                {
                    id_producto = p.id_producto,
                    nombre_producto = p.nombre_producto,
                    stock_actual = p.stock_producto
                })
                .ToListAsync();
        }

        public async Task<List<ProductoSinVentaDTO>> ObtenerProductosSinVentaDesdeAsync(DateTime desde)
        {
            var productosConVenta = await _context.datalle_venta
                .Where(dv => dv.venta!.fecha_venta >= desde)
                .Select(dv => dv.id_producto)
                .Distinct()
                .ToListAsync();

            return await _context.productos
                .Where(p => !productosConVenta.Contains(p.id_producto))
                .Select(p => new ProductoSinVentaDTO
                {
                    id_producto = p.id_producto,
                    nombre_producto = p.nombre_producto
                })
                .ToListAsync();
        }
     
        public async Task<List<EvolucionVentasDTO>> ObtenerEvolucionVentasAsync(string tipo)
        {
            var query = _context.venta.AsQueryable();

            switch (tipo.ToLower().Trim())
            {
                case "diaria":
                {
                    var lista = await query
                        .GroupBy(v => v.fecha_venta.Date)
                        .Select(g => new
                        {
                            fecha = g.Key,
                            total = g.Sum(v => v.total_venta)
                        })
                        .OrderBy(g => g.fecha)
                        .ToListAsync();

                    return lista.Select(g => new EvolucionVentasDTO
                    {
                        periodo = g.fecha.ToString("yyyy-MM-dd"),
                        total = g.total
                    }).ToList();
                }

                case "mensual":
                {
                    var lista = await query
                        .GroupBy(v => new { v.fecha_venta.Year, v.fecha_venta.Month })
                        .Select(g => new
                        {
                            g.Key.Year,
                            g.Key.Month,
                            total = g.Sum(v => v.total_venta)
                        })
                        .OrderBy(g => g.Year).ThenBy(g => g.Month)
                        .ToListAsync();

                    return lista.Select(g => new EvolucionVentasDTO
                    {
                        periodo = $"{g.Year}-{g.Month:D2}",
                        total = g.total
                    }).ToList();
                }
                //esto esta conNotimplemetedException por que la semanal no se puede aplicar por que no no esta desarrollada en el entity
                case "semanal":
                   throw new NotImplementedException("Evolución semanal aún no implementada.");

                default:
                    throw new ArgumentException($"Tipo de evolución no válido: '{tipo}'");
            }
        }

    
        
        public async Task<List<GananciaPorProductoDTO>> ObtenerGananciaPorProductoAsync()
        {
            return await _context.datalle_venta
                .Include(d => d.producto)
                .GroupBy(d => new { d.id_producto, d.producto!.nombre_producto, d.producto.precio_costo })
                .Select(g => new GananciaPorProductoDTO
                {
                    id_producto = g.Key.id_producto,
                    nombre_producto = g.Key.nombre_producto,
                    ganancia_neta = g.Sum(d => (d.precio - g.Key.precio_costo) * d.cantidad)
                })
                .ToListAsync();
        }
    }
}