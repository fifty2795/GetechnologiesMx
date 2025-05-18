using AutoMapper;
using Examen.API.Business.Interfaces;
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
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _facturaService;
        private readonly IMapper _mapper;

        public FacturaController(IFacturaService facturaService, IMapper mapper)
        {
            _facturaService = facturaService;
            _mapper = mapper;
        }

        [HttpGet("obtenerFacturas")]
        public async Task<IActionResult> ObtenerFacturas([FromQuery] string? idFactura)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _facturaService.ObtenerFactura(idFactura);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }

        [HttpPost("obtenerFacturaById")]
        public async Task<IActionResult> ObtenerFacturaById([FromQuery] int? idFactura, [FromQuery] int? idPersona)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _facturaService.ObtenerFacturaByIdFactIdPersona(idFactura.Value, idPersona.Value);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }

        [HttpPost("agregarFactura")]
        public async Task<IActionResult> AgregarFactura([FromBody] FacturaViewModel facturaViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var factura = _mapper.Map<TblFactura>(facturaViewModel);

            factura.Fecha = DateTime.Now;

            var result = await _facturaService.AgregarFactura(factura);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}
