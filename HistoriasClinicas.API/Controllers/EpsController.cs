using AutoMapper;
using HistoriasClinicas.DataAccess;
using HistoriasClinicas.DataAccess.Repositorio.IRepositorio;
using HistoriasClinicas.Models.Modelos;
using HistoriasClinicas.Models.Modelos.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HistoriasClinicas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpsController : ControllerBase
    {
        private ResponseDto _response;
        private readonly ILogger<EpsController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;
        private IMapper _mapper;

        public EpsController(IUnidadTrabajo unidadTrabajo, ILogger<EpsController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
            _response = new ResponseDto();
        }

        // GET: api/Eps
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Eps>>> GetEps()
        {
            _logger.LogInformation("Listado de Eps");
            var lista = await _unidadTrabajo.Eps.ObtenerTodos();
            _response.IsExitoso = true;
            _response.Resultado = lista;
            _response.Mensaje = "Listado de Eps";
            return Ok(_response);
        }

        // GET: api/Eps/5
        [HttpGet("{Id}", Name = "GetEps")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Eps>> GetEps(int Id)
        {
            if (Id == 0)
            {
                _logger.LogError("Debe enviar el ID");
                _response.Mensaje = "Debe enviar el ID";
                _response.IsExitoso = false;
                return BadRequest(_response);
            }

            var eps = await _unidadTrabajo.Eps.ObtenerPrimero(e => e.Id == Id);

            if (eps == null)
            {
                _logger.LogError("Eps no existe");
                _response.Mensaje = "Eps no Existe";
                _response.IsExitoso = false;
                return NotFound(_response);
            }

            _response.Resultado = eps;
            _response.Mensaje = "Informacion del Eps " + eps.Id;
            return Ok(_response); //Status code = 200;
        }

        // POST: api/Eps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Eps>> PostEps([FromBody] EpsDto epsDto)
        {
            if (epsDto == null)
            {
                _response.Mensaje = "Informacion incorrecta";
                _response.IsExitoso = false;
                return BadRequest(_response);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var epsExiste = await _unidadTrabajo.Eps.ObtenerPrimero(
                e => e.NombreEPS.ToLower() == epsDto.NombreEPS.ToLower()
                );

            if (epsExiste != null)
            {
                ModelState.AddModelError("Nombre Duplicado", "Nombre de la eps ya existe");
                return BadRequest(ModelState);
            }
            Eps eps= _mapper.Map<Eps>(epsDto);
            await _unidadTrabajo.Eps.Agregar(eps);
            await _unidadTrabajo.Guardar();
            return CreatedAtRoute("GetEps", new { id = epsDto.Id }, eps); //status code = 201
        }

        // PUT: api/Eps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutEps(int Id, [FromBody] EpsDto epsDto)
        {
            if (Id != epsDto.Id)
            {
                return BadRequest(ModelState);
            }

            var epsExiste = await _unidadTrabajo.Eps.ObtenerPrimero(
                e => e.NombreEPS.ToLower() == epsDto.NombreEPS.ToLower() && e.Id != epsDto.Id);

            if (epsExiste != null)
            {
                ModelState.AddModelError("Nombre Duplicado", "Nombre de la EPS ya existe");
                return BadRequest(ModelState);
            }
            Eps eps= _mapper.Map<Eps>(epsDto);
            _unidadTrabajo.Eps.Actualizar(eps);
            await _unidadTrabajo.Guardar();
            return CreatedAtRoute("GetEps", new { id = eps.Id }, eps); //status code = 201
        }

        // DELETE: api/Eps/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEps(int Id)
        {
            var eps = await _unidadTrabajo.Eps.ObtenerPrimero(e => e.Id == Id);

            if (eps == null)
            {
                _response.IsExitoso = false;
                _response.Mensaje = "Error al elminar";
                _response.Resultado = HttpStatusCode.BadRequest;

                //return NotFound();
            }
            else
            {
                _unidadTrabajo.Eps.Remover(eps);
                await _unidadTrabajo.Guardar();
                _response.IsExitoso = true;
                _response.Mensaje = "Eps Eliminada";
                _response.Resultado = HttpStatusCode.NoContent;
            }
            return Ok(_response);
            
        }
    }
}
