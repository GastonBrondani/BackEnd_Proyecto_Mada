using BackendMada.DTOs.Producto;
using BackendMada.Models;

namespace BackendMada.Service.Interfaces
{
    public interface IProductoService
    {
        Task<List<Producto>> GetAllAsync();
        Task<Producto?> GetByIdAsync(int id);
        Task<Producto> CreateAsync(Producto producto);
        Task<bool> UpdateAsync(int id, Producto producto);
        Task<bool> DeleteAsync(int id);
        Task<List<StockPorProveedorDTO>> GetStockConProveedor(string nombreProducto);
    }
}