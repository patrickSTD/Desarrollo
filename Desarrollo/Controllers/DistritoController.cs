using AutoMapper;
using DB.Models;
using Desarrollo.DTOs;
using Desarrollo.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desarrollo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistritoController : ControllerBase
    {
        private readonly DistritoRepository _distritoRepository;
        private readonly IMapper _mapper;

        public DistritoController(DistritoRepository distritoRepository, IMapper mapper)
        {
            _distritoRepository = distritoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistritoMostrarDto>>> ListarDistritos()
        {
            var distritos = await _distritoRepository.ListarDistritos();
            return Ok(_mapper.Map<IEnumerable<DistritoMostrarDto>>(distritos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DistritoMostrarDto>> BuscarDistritoPorId(int id)
        {
            var distrito = await _distritoRepository.BuscarDistritoPorId(id);
            if (distrito != null)
            {
                return Ok(_mapper.Map<DistritoMostrarDto>(distrito));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<DistritoMostrarDto>> GuardarDistrito(DistritoCrearDto distritoCrearDto)
        {
            var distrito = _mapper.Map<Distrito>(distritoCrearDto);
            await _distritoRepository.GuardarDistrito(distrito);

            var distritoMostrarDto = _mapper.Map<DistritoMostrarDto>(distrito);
            return CreatedAtAction(nameof(BuscarDistritoPorId), new { id = distritoMostrarDto.Id }, distritoMostrarDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDistrito(int id, DistritoCrearDto distritoCrearDto)
        {
            var distrito = await _distritoRepository.BuscarDistritoPorId(id);
            if (distrito == null)
            {
                return NotFound();
            }

            _mapper.Map(distritoCrearDto, distrito);
            await _distritoRepository.ActualizarDistrito(distrito);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDistrito(int id)
        {
            var distrito = await _distritoRepository.BuscarDistritoPorId(id);
            if (distrito == null)
            {
                return NotFound();
            }

            await _distritoRepository.EliminarDistrito(id);
            return NoContent();
        }
    }
}
