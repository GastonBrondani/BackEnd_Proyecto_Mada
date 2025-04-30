using BackendMada.Data;
using BackendMada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Service
{
    [ApiController]
    [Route("api/categoria")]
    public class CategoriaService
    {
        private readonly MyDbContext _context;
        
        public CategoriaService(MyDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("nombre/{nombre}")]
        public async Task<List<Categoria>> ObtenerPorNombre(string nombre)
        {
            return await _context.categoria.Where(c => c.nombre_categoria.ToLower().Contains(nombre.ToLower()))
                .ToListAsync();
        }
        
    }
    
}