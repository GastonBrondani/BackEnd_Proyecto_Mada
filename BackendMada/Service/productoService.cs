using BackendMada.Models;
using BackendMada.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendMada.DTOs.Producto;
using BackendMada.Data;



namespace BackendMada.Service
{
    public class ProductoService : IProductoService
    {
        private readonly MyDbContext _context;

        public ProductoService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> GetAllAsync()
        {
            return await _context.productos.ToListAsync();
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await _context.productos.FindAsync(id);
        }

        public async Task<Producto> CreateAsync(Producto producto)
        {
            _context.productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> UpdateAsync(int id, Producto producto)
        {
            var existe = await _context.productos.AnyAsync(p => p.id_producto == id);
            if (!existe) return false;

            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var producto = await _context.productos.FindAsync(id);
            if (producto == null) return false;

            _context.productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<StockPorProveedorDTO>> GetStockConProveedor(string nombreProducto)
        {
            var productosFiltrados = await _context.productos
                .Where(p => p.nombre_producto.Contains(nombreProducto))
                .Include(p => p.id_proveedorNavigation)
                .Select(p => new StockPorProveedorDTO
                {
                    nombre_producto = p.nombre_producto,
                    stock_producto = p.stock_producto,
                    nombre_proveedor = p.id_proveedorNavigation != null ? p.id_proveedorNavigation.nombre_proveedor : "Sin proveedor"
                })
                .ToListAsync();

            if (!productosFiltrados.Any())
                throw new KeyNotFoundException("No se encontraron productos con ese nombre.");

            return productosFiltrados;
        }
    }
}