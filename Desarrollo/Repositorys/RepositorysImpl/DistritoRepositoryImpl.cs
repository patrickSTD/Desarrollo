using DB.Data;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Desarrollo.Repositorys.RepositorysImpl
{
    public class DistritoRepositoryImpl : DistritoRepository
    {
        private readonly TrabajadoresContext _context;

        public DistritoRepositoryImpl(TrabajadoresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Distrito>> ListarDistritos()
        {
            return await _context.Distritos
                .Include(distrito => distrito.IdProvinciaNavigation)
                .ThenInclude(provincia => provincia.IdDepartamentoNavigation)
                .ToListAsync();
        }

        public async Task<Distrito> BuscarDistritoPorId(int id)
        {
            return await _context.Distritos
                .Include(distrito => distrito.IdProvinciaNavigation)
                .ThenInclude(provincia => provincia.IdDepartamentoNavigation)
                .FirstOrDefaultAsync(distrito => distrito.Id == id) ?? 
                throw new KeyNotFoundException($"Distrito con ID {id} no encontrado.");
        }

        public async Task GuardarDistrito(Distrito distrito)
        {
            if (distrito == null)
            {
                throw new ArgumentNullException(nameof(distrito));
            }

            await _context.Distritos.AddAsync(distrito);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarDistrito(Distrito distrito)
        {
            await _context.SaveChangesAsync();
        }

        public async Task EliminarDistrito(int id)
        {
            var distrito = await _context.Distritos.FindAsync(id);
            if (distrito == null)
            {
                throw new ArgumentNullException(nameof(distrito));
            }

            _context.Distritos.Remove(distrito);
            await _context.SaveChangesAsync();
        }
    }
}
