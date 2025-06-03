using BackendMada.Models;

namespace BackendMada.Service.Interfaces;

public interface iProveedorService
{
    Task<List<Proveedor>> ObtenerTodos();
    Task<Proveedor?> ObtenerPorId(int id);
    Task<Proveedor?> ObtenerPorCuit(string cuit);
    Task<List<Proveedor>> BuscarPorNombre(string nombre);
    Task<bool> ExisteCuit(string cuit);
    Task<Proveedor> CrearProveedor(Proveedor proveedor);
    Task<bool> ActualizarProveedor(Proveedor proveedor);
    Task<bool> EliminarProveedor(int id);
    bool EsCuitValido(string cuit);

}