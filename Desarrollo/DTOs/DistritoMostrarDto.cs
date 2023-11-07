namespace Desarrollo.DTOs
{
    public class DistritoMostrarDto
    {
        public int Id { get; set; }
        public string NombreDistrito { get; set; }
        public ProvinciaMostrarDto? Provincia { get; set; }
    }
}
