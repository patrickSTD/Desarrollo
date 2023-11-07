using AutoMapper;
using DB.Data;
using DB.Models;
using Desarrollo.DTOs;
using Desarrollo.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desarrollo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly DepartamentoRepository _repository;
        private readonly IMapper _mapper;

        public DepartamentoController(DepartamentoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartamentoMostrarDto>>> ListarDepartamentos()
        {
            var departamentos = await _repository.ListarDepartamentos();
            return Ok(_mapper.Map<IEnumerable<DepartamentoMostrarDto>>(departamentos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartamentoMostrarDto>> GetDepartamentoById(int id)
        {
            var departamento = await _repository.BuscarDepartamentoPorId(id);
            if (departamento != null)
            {
                return Ok(_mapper.Map<DepartamentoMostrarDto>(departamento));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<DepartamentoMostrarDto>> GuardarDepartamento(DepartamentoCrearDto departamentoCrearDto)
        {
            var departamento = _mapper.Map<Departamento>(departamentoCrearDto);
            await _repository.GuardarDepartamento(departamento);

            var departamentoMostrarDto = _mapper.Map<DepartamentoMostrarDto>(departamento);
            return CreatedAtAction(nameof(GetDepartamentoById), new { id = departamentoMostrarDto.Id }, departamentoMostrarDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDepartamento(int id, DepartamentoCrearDto departamentoCrearDto)
        {
            var departamento = await _repository.BuscarDepartamentoPorId(id);
            if (departamento == null)
            {
                return NotFound();
            }

            _mapper.Map(departamentoCrearDto, departamento);
            await _repository.ActualizarDepartamento(departamento);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDepartamento(int id)
        {
            var departamento = await _repository.BuscarDepartamentoPorId(id);
            if (departamento == null)
            {
                return NotFound();
            }

            await _repository.EliminarDepartamento(id);
            return NoContent();
        }
    }
}
