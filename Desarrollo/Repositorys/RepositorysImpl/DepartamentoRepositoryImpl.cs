using DB.Data;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Desarrollo.Repositorys.RepositorysImpl
{
    public class DepartamentoRepositoryImpl :DepartamentoRepository
    {
        private readonly TrabajadoresContext _context;

        public DepartamentoRepositoryImpl(TrabajadoresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Departamento>> ListarDepartamentos()
        {
            return await _context.Departamentos
                .Include(dep => dep.Provincia)
                .ThenInclude(prov => prov.Distritos)
                .ToListAsync();
        }

        public async Task<Departamento> BuscarDepartamentoPorId(int id)
        {
            return await _context.Departamentos
                .Include(dep => dep.Provincia)
                .ThenInclude(prov => prov.Distritos)
                .FirstOrDefaultAsync(dep => dep.Id == id) ?? throw new KeyNotFoundException($"Departamento con ID {id} no encontrado.");
        }

        public async Task GuardarDepartamento(Departamento departamento)
        {
            if (departamento == null)
            {
                throw new ArgumentNullException(nameof(departamento));
            }
            await _context.Departamentos.AddAsync(departamento);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarDepartamento(Departamento departamento)
        {
            await _context.SaveChangesAsync();
        }

        public async Task EliminarDepartamento(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento == null)
            {
                throw new ArgumentNullException(nameof(departamento));
            }
            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();
        }
    }
}
