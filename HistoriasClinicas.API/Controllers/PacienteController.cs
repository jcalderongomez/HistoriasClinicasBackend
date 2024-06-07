using AutoMapper;
using HistoriasClinicas.DataAccess;
using HistoriasClinicas.DataAccess.Repositorio;
using HistoriasClinicas.DataAccess.Repositorio.Interfaces;
using HistoriasClinicas.Models.Modelos;
using HistoriasClinicas.Models.Modelos.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HistoriasClinicas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private ResponseDto _response;
        private readonly ILogger<PacienteController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;
        private IMapper _mapper;

        

        public PacienteController(IUnidadTrabajo unidadTrabajo, ILogger<PacienteController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
           _response = new ResponseDto();
        }

        // GET: api/Pacientes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            _logger.LogInformation("Listado de Pacientes");
            var lista = await _unidadTrabajo.Paciente.ObtenerTodos(incluirPropiedades:"Eps");
            _response.Resultado = lista;
            _response.Mensaje = ("Listado de Pacientes");
            return Ok(lista);
        }

        // GET: api/Pacientes/5
        [HttpGet("{Id}", Name="GetPaciente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Paciente>> GetPaciente(int Id)
        {
            if (Id == 0) {
                _logger.LogError("Debe enviar el ID");
                _response.Mensaje = "Debe enviar el ID";
                _response.IsExitoso = false;
                return BadRequest(_response);
            }


            var paciente = await _unidadTrabajo.Paciente.ObtenerPrimero(p => p.Id==Id, incluirPropiedades:"Eps");
            
            if (paciente == null)
            {
                _logger.LogError("Paciente no existe");
                _response.Mensaje = "Paciente no Existe";
                _response.IsExitoso= false;
                return NotFound(_response);
            }

            _response.Resultado = paciente;
            _response.Mensaje= "Informacion del Paciente "+paciente.Id;
            return Ok(_response); //Status code = 200;
        }

        // POST: api/Pacientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Paciente>> PostPaciente([FromBody]PacienteUpsertDto pacienteUpsertDto)
        {
            if (pacienteUpsertDto == null) {
                _response.Mensaje = "Informacion incorrecta";
                _response.IsExitoso = false;
                return BadRequest(_response);
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var pacienteExiste = await _unidadTrabajo.Paciente.ObtenerPrimero(
                p=> p.Nombres.ToLower() == pacienteUpsertDto.Nombres.ToLower()
                );

            if (pacienteExiste != null) {
                ModelState.AddModelError("Nombre Duplicado", "Nombre del paciente ya existe");
                return BadRequest(ModelState);
            }
            Paciente paciente = _mapper.Map<Paciente>(pacienteUpsertDto);
            await _unidadTrabajo.Paciente.Agregar(paciente);
            await _unidadTrabajo.Guardar();
            return CreatedAtRoute("GetPaciente", new { Id = paciente.Id }, paciente); //status code = 201
        }

        // PUT: api/Pacientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, [FromBody] PacienteUpsertDto pacienteUpsertDto)
        {
            if (id != pacienteUpsertDto.Id) {
                return BadRequest(ModelState);
            }

            var pacienteExiste = await _unidadTrabajo.Paciente.ObtenerPrimero(
                p => p.Nombres.ToLower() == pacienteUpsertDto.Nombres.ToLower() && p.Id != pacienteUpsertDto.Id);

            if (pacienteExiste != null)
            {
                ModelState.AddModelError("Nombre Duplicado", "Nombre del paciente ya existe");
                return BadRequest(ModelState);
            }
            Paciente paciente = _mapper.Map<Paciente>(pacienteUpsertDto);
            await _unidadTrabajo.Paciente.Agregar(paciente);
            await _unidadTrabajo.Guardar();
            return CreatedAtRoute("GetPaciente", new { id = paciente.Id }, paciente); //status code = 201
        }



        // DELETE: api/Pacientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await _unidadTrabajo.Paciente.ObtenerPrimero(p => p.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }
            _unidadTrabajo.Paciente.Remover(paciente);
            await _unidadTrabajo.Guardar();
            return NoContent();
        }

       
    }
}
