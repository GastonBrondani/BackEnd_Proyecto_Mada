using BackendMada.Models;

namespace BackendMada.Service.Interfaces;

public interface ICategoriaService
{
          Task<List<Categoria>> GetAllAsync();
          
        Task<Categoria?> GetByIdAsync(int id);
        
     Task<Categoria> CreateAsync(Categoria categoria);
     
     Task<bool> UpdateAsync(int id, Categoria categoria);
     
     Task<bool> DeleteAsync(int id);
     Task<List<Categoria>> ObtenerPorNombre(string nombre);
    
}