using BackendMada.Data;
using BackendMada.Models;
using BackendMada.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Service
{
    [ApiController]
    [Route("api/categoria")]
    public class CategoriaService:ICategoriaService
    {
        private readonly MyDbContext _context;
        
        public CategoriaService(MyDbContext context)
        {
            _context = context;
        }
        public async Task<List<Categoria>> GetAllAsync()
        {
            return await _context.categoria.ToListAsync();
        }
        public async Task<Categoria?> GetByIdAsync(int id)
        {
            return await _context.categoria.FindAsync(id);
        }
        public async Task<Categoria> CreateAsync(Categoria categoria)
        {
            _context.categoria.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }
        
        public async Task<bool> UpdateAsync(int id, Categoria categoria)
        {
            var existe = await _context.categoria.AnyAsync(c => c.id_categoria == id);
            if (!existe) return false;

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var categoria = await _context.categoria.FindAsync(id);
            if (categoria == null) return false;

            _context.categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<List<Categoria>> ObtenerPorNombre(string nombre) =>
            await _context.categoria
                .Where(c => c.nombre_categoria.ToLower().Contains(nombre.ToLower()))
                .ToListAsync();
        
        
        
        
    }
    
}