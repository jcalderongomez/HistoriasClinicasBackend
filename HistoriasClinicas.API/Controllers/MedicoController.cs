using AutoMapper;
using HistoriasClinicas.DataAccess.Repositorio.IRepositorio;
using HistoriasClinicas.Models.Modelos.Dto;
using HistoriasClinicas.Models.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace HistoriasClinicas.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private ResponseDto _response;
        private readonly ILogger<MedicoController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;
        private IMapper _mapper;

        public MedicoController(IUnidadTrabajo unidadTrabajo, ILogger<MedicoController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
            _response = new ResponseDto();
        }

        // GET: api/Medicos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Medico>>> GetMedicos()
        {
            _logger.LogInformation("Listado de Medicos");
            var lista = await _unidadTrabajo.Medico.ObtenerTodos(incluirPropiedades: "Especialidad");
            _response.IsExitoso = true;
            _response.Resultado = lista;
            _response.Mensaje = "Listado de Medicos";
            return Ok(_response);
        }

        // GET: api/Medicos/5
        [HttpGet("{Id}", Name = "GetMedico")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Medico>> GetMedico(int Id)
        {
            if (Id == 0)
            {
                _logger.LogError("Debe enviar el ID");
                _response.Mensaje = "Debe enviar el ID";
                _response.IsExitoso = false;
                return BadRequest(_response);
            }

            var medico = await _unidadTrabajo.Medico.ObtenerPrimero(p => p.Id == Id, incluirPropiedades: "Eps");

            if (medico == null)
            {
                _logger.LogError("Medico no existe");
                _response.Mensaje = "Medico no Existe";
                _response.IsExitoso = false;
                return NotFound(_response);
            }

            _response.Resultado = medico;
            _response.Mensaje = "Informacion del Medico " + medico.Id;
            return Ok(_response); //Status code = 200;
        }

        // POST: api/Medicos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Medico>> PostMedico([FromBody] MedicoUpsertDto medicoUpsertDto)
        {
            if (medicoUpsertDto == null)
            {
                _response.Mensaje = "Informacion incorrecta";
                _response.IsExitoso = false;
                return BadRequest(_response);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var medicoExiste = await _unidadTrabajo.Medico.ObtenerPrimero(
                p => p.Nombres.ToLower() == medicoUpsertDto.Nombres.ToLower()
                );

            if (medicoExiste != null)
            {
                ModelState.AddModelError("Nombre Duplicado", "Nombre del medico ya existe");
                return BadRequest(ModelState);
            }
            Medico medico = _mapper.Map<Medico>(medicoUpsertDto);
            await _unidadTrabajo.Medico.Agregar(medico);
            await _unidadTrabajo.Guardar();
            return CreatedAtRoute("GetMedico", new { Id = medico.Id }, medico); //status code = 201
        }

        // PUT: api/Medicos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedico(int id, [FromBody] MedicoUpsertDto medicoUpsertDto)
        {
            if (id != medicoUpsertDto.Id)
            {
                return BadRequest(ModelState);
            }

            var medicoExiste = await _unidadTrabajo.Medico.ObtenerPrimero(
                p => p.Nombres.ToLower() == medicoUpsertDto.Nombres.ToLower() && p.Id != medicoUpsertDto.Id);

            if (medicoExiste != null)
            {
                ModelState.AddModelError("Nombre Duplicado", "Nombre del medico ya existe");
                return BadRequest(ModelState);
            }
            Medico medico = _mapper.Map<Medico>(medicoUpsertDto);
            await _unidadTrabajo.Medico.Agregar(medico);
            await _unidadTrabajo.Guardar();
            return CreatedAtRoute("GetMedico", new { id = medico.Id }, medico); //status code = 201
        }



        // DELETE: api/Medicos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedico(int id)
        {
            var medico = await _unidadTrabajo.Medico.ObtenerPrimero(p => p.Id == id);

            if (medico == null)
            {
                return NotFound();
            }
            _unidadTrabajo.Medico.Remover(medico);
            await _unidadTrabajo.Guardar();
            return NoContent();
        }


    }
}
