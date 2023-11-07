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
    public class ProvinciaController : ControllerBase
    {
        private readonly ProvinciaRepository _provinciaRepository;
        private readonly IMapper _mapper;

        public ProvinciaController(ProvinciaRepository provinciaRepository, IMapper mapper)
        {
            _provinciaRepository = provinciaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvinciaMostrarDto>>> ListarProvincias()
        {
            var provincias = await _provinciaRepository.ListarProvincias();
            return Ok(_mapper.Map<IEnumerable<ProvinciaMostrarDto>>(provincias));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProvinciaMostrarDto>> BuscarProvinciaPorId(int id)
        {
            var provincia = await _provinciaRepository.BuscarProvinciaPorId(id);
            if (provincia != null)
            {
                return Ok(_mapper.Map<ProvinciaMostrarDto>(provincia));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ProvinciaMostrarDto>> GuardarProvincia(ProvinciaCrearDto provinciaCrearDto)
        {
            var provincia = _mapper.Map<Provincia>(provinciaCrearDto);
            await _provinciaRepository.GuardarProvincia(provincia);

            var provinciaMostrarDto = _mapper.Map<ProvinciaMostrarDto>(provincia);
            return CreatedAtAction(nameof(BuscarProvinciaPorId), new { id = provinciaMostrarDto.Id }, provinciaMostrarDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProvincia(int id, ProvinciaCrearDto provinciaCrearDto)
        {
            var provincia = await _provinciaRepository.BuscarProvinciaPorId(id);
            if (provincia == null)
            {
                return NotFound();
            }

            _mapper.Map(provinciaCrearDto, provincia);
            await _provinciaRepository.ActualizarProvincia(provincia);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProvincia(int id)
        {
            var provincia = await _provinciaRepository.BuscarProvinciaPorId(id);
            if (provincia == null)
            {
                return NotFound();
            }

            await _provinciaRepository.EliminarProvincia(id);
            return NoContent();
        }
    }
}
