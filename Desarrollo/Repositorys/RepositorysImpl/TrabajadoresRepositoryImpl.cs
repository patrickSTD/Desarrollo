using DB.Data;
using DB.Models;
using Desarrollo.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Desarrollo.Repositorys.RepositorysImpl
{
    public class TrabajadoresRepositoryImpl : TrabajadoresRepository
    {
        private readonly TrabajadoresContext _context;

        public TrabajadoresRepositoryImpl(TrabajadoresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trabajadores>> ListarTrabajadores()
        {
            return await _context.Trabajadores
                .FromSqlRaw("EXEC ListarTrabajadores")
                .ToListAsync();
        }

        public async Task<Trabajadores> BuscarTrabajadorPorId(int id)
        {

            var trabajador = await _context.Trabajadores
                .Include(t => t.IdDepartamentoNavigation)
                .Include(t => t.IdProvinciaNavigation)
                .Include(t => t.IdDistritoNavigation)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trabajador == null)
            {
                throw new KeyNotFoundException($"Trabajador con ID {id} no encontrado.");
            }

            return trabajador;
        }

        public async Task GuardarTrabajador(Trabajadores trabajador)
        {
            if (trabajador == null)
            {
                throw new ArgumentNullException(nameof(trabajador));
            }

            await _context.Trabajadores.AddAsync(trabajador);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarTrabajador(Trabajadores trabajador)
        {
            await _context.SaveChangesAsync();
        }

        public async Task EliminarTrabajador(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null)
            {
                throw new ArgumentNullException(nameof(trabajador));
            }

            _context.Trabajadores.Remove(trabajador);
            await _context.SaveChangesAsync();
        }
    }
}
