using BackendMada.Data;
using BackendMada.Models;
using BackendMada.Service.Interfaces;
using BackendMada.Utils;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Service
{
    public class ProveedorService: iProveedorService
    {
        private readonly MyDbContext _context;

        public ProveedorService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Proveedor>> ObtenerTodos()
        {
            return await _context.proveedores.ToListAsync();
        }

        public async Task<Proveedor?> ObtenerPorId(int id)
        {
            return await _context.proveedores.FindAsync(id);
        }

        public async Task<Proveedor?> ObtenerPorCuit(string cuit)
        {
            return await _context.proveedores.FirstOrDefaultAsync(p => p.cuil_proveedor == cuit);
        }

        public async Task<List<Proveedor>> BuscarPorNombre(string nombre)
        {
            return await _context.proveedores
                .Where(p => p.nombre_proveedor.ToLower().Contains(nombre.ToLower()))
                .ToListAsync();
        }

        public bool EsCuitValido(string cuit)
        {
            return ValidadorCUIT.EsCuitValido(cuit);
        }

        public async Task<bool> ExisteCuit(string cuit)
        {
            return await _context.proveedores.AnyAsync(p => p.cuil_proveedor == cuit);
        }

        public async Task<Proveedor> CrearProveedor(Proveedor proveedor)
        {
            _context.proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        public async Task<bool> ActualizarProveedor(Proveedor proveedor)
        {
            if (!await _context.proveedores.AnyAsync(p => p.id_proveedor == proveedor.id_proveedor))
                return false;

            _context.Entry(proveedor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarProveedor(int id)
        {
            var proveedor = await _context.proveedores.FindAsync(id);
            if (proveedor == null)
                return false;

            _context.proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
