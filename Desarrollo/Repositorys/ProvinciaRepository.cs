using DB.Models;

namespace Desarrollo.Repositorys
{
    public interface ProvinciaRepository
    {
        Task<IEnumerable<Provincia>> ListarProvincias();
        Task<Provincia> BuscarProvinciaPorId(int id);
        Task GuardarProvincia(Provincia provincia);
        Task ActualizarProvincia(Provincia provincia);
        Task EliminarProvincia(int id);
    }
}
