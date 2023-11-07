using AutoMapper;
using DB.Data;
using DB.Models;
using Desarrollo.DTOs;
using Desarrollo.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desarrollo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadoresController : ControllerBase
    {
        private readonly TrabajadoresRepository _trabajadoresRepository;
        private readonly IMapper _mapper;

        public TrabajadoresController(TrabajadoresRepository trabajadoresRepository, IMapper mapper)
        {
            _trabajadoresRepository = trabajadoresRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrabajadoresMostrarDto>>> ListarTrabajadores()
        {
            var trabajadores = await _trabajadoresRepository.ListarTrabajadores();
            return Ok(_mapper.Map<IEnumerable<TrabajadoresMostrarDto>>(trabajadores));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrabajadoresMostrarDto>> BuscarTrabajadorPorId(int id)
        {
            var trabajador = await _trabajadoresRepository.BuscarTrabajadorPorId(id);
            if (trabajador != null)
            {
                return Ok(_mapper.Map<TrabajadoresMostrarDto>(trabajador));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<TrabajadoresMostrarDto>> GuardarTrabajador(TrabajadoresCrearDto trabajadoresCrearDto)
        {
            var trabajador = _mapper.Map<Trabajadores>(trabajadoresCrearDto);
            await _trabajadoresRepository.GuardarTrabajador(trabajador);
            var trabajadoresMostrarDto = _mapper.Map<TrabajadoresMostrarDto>(trabajador);

            return CreatedAtAction(nameof(BuscarTrabajadorPorId), new { id = trabajadoresMostrarDto.Id }, trabajadoresMostrarDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarTrabajador(int id, TrabajadoresCrearDto trabajadoresCrearDto)
        {
            var trabajador = await _trabajadoresRepository.BuscarTrabajadorPorId(id);
            if (trabajador == null)
            {
                return NotFound();
            }

            _mapper.Map(trabajadoresCrearDto, trabajador);
            await _trabajadoresRepository.ActualizarTrabajador(trabajador);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTrabajador(int id)
        {
            var trabajador = await _trabajadoresRepository.BuscarTrabajadorPorId(id);
            if (trabajador == null)
            {
                return NotFound();
            }

            await _trabajadoresRepository.EliminarTrabajador(id);
            return NoContent();
        }
    }
}
