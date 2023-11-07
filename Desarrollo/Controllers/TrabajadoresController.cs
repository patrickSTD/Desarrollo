using DB.Data;
using DB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desarrollo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadoresController : ControllerBase
    {
        private TrabajadoresContext _context;

        public TrabajadoresController(TrabajadoresContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Trabajadores> Get() => _context.Trabajadores.ToList();
    }
}
