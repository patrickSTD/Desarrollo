using DB.Models;

namespace Desarrollo.Repositorys
{
    public interface DepartamentoRepository
    {
        Task<IEnumerable<Departamento>> ListarDepartamentos();
        Task<Departamento> BuscarDepartamentoPorId(int id);
        Task GuardarDepartamento(Departamento departamento);
        Task ActualizarDepartamento(Departamento departamento);
        Task EliminarDepartamento(int id);
    }
}
