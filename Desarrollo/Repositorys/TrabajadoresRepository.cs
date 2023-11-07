using DB.Models;
using Desarrollo.DTOs;

namespace Desarrollo.Repositorys
{
    public interface TrabajadoresRepository
    {
        Task<IEnumerable<Trabajadores>> ListarTrabajadores();
        Task<Trabajadores> BuscarTrabajadorPorId(int id);
        Task GuardarTrabajador(Trabajadores trabajadores);
        Task ActualizarTrabajador(Trabajadores trabajadores);
        Task EliminarTrabajador(int id);
    }
}
