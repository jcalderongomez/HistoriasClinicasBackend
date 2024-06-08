using AutoMapper;
using HistoriasClinicas.DataAccess;
using HistoriasClinicas.DataAccess.Repositorio.Interfaces;
using HistoriasClinicas.Models.Modelos;
using HistoriasClinicas.Models.Modelos.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HistoriasClinicas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private ResponseDto _response;
        private readonly ILogger<EspecialidadController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;
        private IMapper _mapper;

        public EspecialidadController(IUnidadTrabajo unidadTrabajo, ILogger<EspecialidadController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
            _response = new ResponseDto();
        }

        // GET: api/Especialidad
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Especialidad>>> GetEspecialidad()
        {
            _logger.LogInformation("Listado de Especialidades");
            var lista = await _unidadTrabajo.Especialidad.ObtenerTodos();
            _response.IsExitoso = true;
            _response.Resultado = lista;
            _response.Mensaje = "Listado de Especialidades";
            return Ok(_response);
        }

        // GET: api/Especialidad/5
        [HttpGet("{Id}", Name = "GetEspecialidad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Especialidad>> GetEspecialidad(int Id)
        {
            if (Id == 0)
            {
                _logger.LogError("Debe enviar el ID");
                _response.Mensaje = "Debe enviar el ID";
                _response.IsExitoso = false;
                return BadRequest(_response);
            }

            var especialidad = await _unidadTrabajo.Especialidad.ObtenerPrimero(e => e.Id == Id);

            if (especialidad == null)
            {
                _logger.LogError("Especialidad no existe");
                _response.Mensaje = "Especialidad no Existe";
                _response.IsExitoso = false;
                return NotFound(_response);
            }

            _response.Resultado = especialidad;
            _response.Mensaje = "Informacion de la Especialidad " + especialidad.Id;
            return Ok(_response); //Status code = 200;
        }

        // POST: api/Especialidad
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Especialidad>> PostEspecialidad([FromBody] EspecialidadDto especialidadDto)
        {
            if (especialidadDto == null)
            {
                _response.Mensaje = "Informacion incorrecta";
                _response.IsExitoso = false;
                return BadRequest(_response);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var especialidadExiste = await _unidadTrabajo.Especialidad.ObtenerPrimero(
                e => e.NombreEspecialidad.ToLower() == especialidadDto.NombreEspecialidad.ToLower()
                );

            if (especialidadExiste != null)
            {
                ModelState.AddModelError("Nombre Duplicado", "Nombre de la especialidad ya existe");
                return BadRequest(ModelState);
            }
            
            Especialidad especialidad = _mapper.Map<Especialidad>(especialidadDto);
            await _unidadTrabajo.Especialidad.Agregar(especialidad);
            await _unidadTrabajo.Guardar();
            return CreatedAtRoute("GetEspecialidad", new { id = especialidadDto.Id }, especialidad); //status code = 201
        }

        // PUT: api/Especialidad/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutEspecialidad(int Id, [FromBody] EspecialidadDto especialidadDto)
        {
            if (Id != especialidadDto.Id)
            {
                return BadRequest(ModelState);
            }

            var especialidadExiste = await _unidadTrabajo.Especialidad.ObtenerPrimero(
                e => e.NombreEspecialidad.ToLower() == especialidadDto.NombreEspecialidad.ToLower() && e.Id != especialidadDto.Id);

            if (especialidadExiste != null)
            {
                ModelState.AddModelError("Nombre Duplicado", "Nombre de la Especialidad ya existe");
                return BadRequest(ModelState);
            }
            Especialidad especialidad = _mapper.Map<Especialidad>(especialidadDto);
            _unidadTrabajo.Especialidad.Actualizar(especialidad);
            await _unidadTrabajo.Guardar();
            return CreatedAtRoute("GetEspecialidad", new { Id = especialidad.Id }, especialidad); //status code = 201
        }

        // DELETE: api/Especialidad/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEspecialidad(int Id)
        {
            var especialidad = await _unidadTrabajo.Especialidad.ObtenerPrimero(e => e.Id == Id);

            if (especialidad == null)
            {
                _response.IsExitoso = false;
                _response.Mensaje = "Error al elminar";
                _response.Resultado = HttpStatusCode.BadRequest;
            }
            else
            {
                _unidadTrabajo.Especialidad.Remover(especialidad);
                await _unidadTrabajo.Guardar();
                _response.IsExitoso = true;
                _response.Mensaje = "Especialidad Eliminada";
                _response.Resultado = HttpStatusCode.NoContent;
            }
            return Ok(_response);
        }
    }
}
