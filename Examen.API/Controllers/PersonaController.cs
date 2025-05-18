using AutoMapper;
using Examen.API.Business.Interfaces;
using Examen.API.Data.Interfaces;
using Examen.API.Data.Models;
using Examen.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _personaService;
        private readonly IMapper _mapper;

        public PersonaController(IPersonaService personaService, IMapper mapper)
        {
            _personaService = personaService;
            _mapper = mapper;
        }

        [HttpGet("obtenerPersonas")]
        public async Task<IActionResult> ObtenerPersonas([FromQuery] string? nombre)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _personaService.ObtenerPersona(nombre);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }

        [HttpPost("obtenerPersonaById")]
        public async Task<IActionResult> ObtenerPersonaById([FromQuery] int? idPersona)
        {
            if (!idPersona.HasValue)
                return BadRequest("El parámetro ID es obligatorio.");

            if (idPersona <= 0)
                return BadRequest("El parámetro ID debe ser mayor que cero.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _personaService.ObtenerPersonaById(idPersona.Value);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }

        [HttpPost("agregarPersona")]
        public async Task<IActionResult> AgregarPersona([FromBody] PersonaViewModel personaViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var persona = _mapper.Map<TblPersona>(personaViewModel);

            var result = await _personaService.AgregarPersona(persona);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }

        [HttpPost("editarPersona")]
        public async Task<IActionResult> EditarPersona([FromBody] PersonaViewModel personaViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!personaViewModel.Id.HasValue)
                return BadRequest("El parámetro ID es obligatorio.");

            var persona = _mapper.Map<TblPersona>(personaViewModel);

            var result = await _personaService.EditarPersona(persona);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }

        [HttpPost("eliminarPersona")]
        public async Task<IActionResult> EliminarPersona([FromQuery] int idPersona)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);            

            var result = await _personaService.EliminarPersona(idPersona);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}
