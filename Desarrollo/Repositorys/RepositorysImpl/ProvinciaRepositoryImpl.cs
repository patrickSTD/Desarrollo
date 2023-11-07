using DB.Data;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Desarrollo.Repositorys.RepositorysImpl
{
    public class ProvinciaRepositoryImpl : ProvinciaRepository
    {
        private readonly TrabajadoresContext _context;

        public ProvinciaRepositoryImpl(TrabajadoresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Provincia>> ListarProvincias()
        {
            return await _context.Provincia
                .Include(provincia => provincia.IdDepartamentoNavigation)
                .ToListAsync();
        }

        public async Task<Provincia> BuscarProvinciaPorId(int id)
        {
            return await _context.Provincia
                .Include(provincia => provincia.IdDepartamentoNavigation)
                .FirstOrDefaultAsync(provincia => provincia.Id == id) ?? throw new KeyNotFoundException($"Provincia con ID {id} no encontrado.");
        }

        public async Task GuardarProvincia(Provincia provincia)
        {
            if (provincia == null)
            {
                throw new ArgumentNullException(nameof(provincia));
            }

            await _context.Provincia.AddAsync(provincia);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarProvincia(Provincia provincia)
        {
            await _context.SaveChangesAsync();
        }

        public async Task EliminarProvincia(int id)
        {
            var provincia = await _context.Provincia.FindAsync(id);
            if (provincia == null)
            {
                throw new ArgumentNullException(nameof(provincia));
            }

            _context.Provincia.Remove(provincia);
            await _context.SaveChangesAsync();
        }
    }
}
