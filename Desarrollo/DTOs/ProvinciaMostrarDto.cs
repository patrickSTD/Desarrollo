using DB.Models;

namespace Desarrollo.DTOs
{
    public class ProvinciaMostrarDto
    {
        public int Id { get; set; }
        public string NombreProvincia { get; set; }
        public DepartamentoMostrarDto? Departamento { get; set; }
    }
}
