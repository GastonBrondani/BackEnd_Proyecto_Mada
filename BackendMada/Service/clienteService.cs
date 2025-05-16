
using BackendMada.Data;
using BackendMada.Models;
using BackendMada.Utils;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Service
{
    public class ClienteService
    {
        private readonly MyDbContext _context;

        public ClienteService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> ObtenerTodosLosClientes() =>
            await _context.clientes.ToListAsync();

        public async Task<List<Cliente>> FiltrarPorApellido(string apellido) =>
            await _context.clientes
                .Where(c => c.nombre.ToLower().Contains(apellido.ToLower()))
                .ToListAsync();

        public async Task<Cliente?> FiltrarPorCuit(string cuit) =>
            await _context.clientes.FirstOrDefaultAsync(c => c.cuit_cuil == cuit);

        public async Task<(bool Exitoso, string? Mensaje, Cliente? Cliente)> CrearCliente(Cliente cliente)
        {
            if (!ValidadorCUIT.EsCuitValido(cliente.cuit_cuil))
                return (false, "CUIT inválido: formato incorrecto o dígito verificador inválido.", null);

            if (await _context.clientes.AnyAsync(c => c.cuit_cuil == cliente.cuit_cuil))
                return (false, "Ya existe un cliente con ese CUIT.", null);

            _context.clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return (true, null, cliente);
        }

        public async Task<bool> ActualizarCliente(Cliente cliente)
        {
            if (!_context.clientes.Any(c => c.id_cliente == cliente.id_cliente))
                return false;

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarCliente(int id)
        {
            var cliente = await _context.clientes.FindAsync(id);
            if (cliente == null)
                return false;

            _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
