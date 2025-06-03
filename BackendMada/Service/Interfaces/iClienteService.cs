using BackendMada.Models;

namespace BackendMada.Service.Interfaces;

public interface IClienteService
{
    Task<List<Cliente>> ObtenerTodosLosClientes();
    Task<List<Cliente>> FiltrarPorApellido(string apellido);
    Task<Cliente?> FiltrarPorCuit(string cuit);
    Task<(bool Exitoso, string? Mensaje, Cliente? Cliente)> CrearCliente(Cliente cliente);
    Task<bool> ActualizarCliente(Cliente cliente);
    Task<bool> EliminarCliente(int id);
}