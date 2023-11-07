using DB.Models;

namespace Desarrollo.Repositorys
{
    public interface DistritoRepository
    {
        Task<IEnumerable<Distrito>> ListarDistritos();
        Task<Distrito> BuscarDistritoPorId(int id);
        Task GuardarDistrito(Distrito distrito);
        Task ActualizarDistrito(Distrito distrito);
        Task EliminarDistrito(int id);
    }
}
