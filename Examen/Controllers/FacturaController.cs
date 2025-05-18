using AutoMapper;
using Examen.Business.DTO;
using Examen.Business.Interfaces;
using Examen.Models;
using Examen.Utilidades.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen.Controllers
{
    public class FacturaController : Controller
    {

        private readonly IFacturaService _facturaService;
        private readonly IPersonaService _personaService;
        private readonly IMapper _mapper;

        public FacturaController(IFacturaService facturaService, IPersonaService personaService, IMapper mapper)
        {
            _facturaService = facturaService;
            _personaService = personaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string? txtBusqueda, int pageNumber)
        {
            int pageSize = 5;

            ViewBag.Busqueda = txtBusqueda;

            if (string.IsNullOrEmpty(txtBusqueda))
            {
                txtBusqueda = string.Empty;
            }

            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            var token = User.FindFirst("AccessToken")?.Value;
            var response = await _facturaService.ObtenerFacturas(token, txtBusqueda, pageNumber, pageSize);

            if (!response.Success)
            {
                var modelError = new ResponseViewModel<PaginatedList<FacturaViewModel>>
                {
                    Data = new PaginatedList<FacturaViewModel>(),
                    Success = response.Success,
                    Message = response.Message,
                    Error = response.Error
                };

                return View(modelError);
            }

            var paginatedViewModel = new PaginatedList<FacturaViewModel>(
                        _mapper.Map<List<FacturaViewModel>>(response.Data.Items),
                        response.Data.TotalCount,
                        pageNumber,
                        pageSize);

            var model = new ResponseViewModel<PaginatedList<FacturaViewModel>>
            {
                Success = response.Success,
                Message = response.Message,
                Data = paginatedViewModel,
                Error = response.Error
            };

            return View(model);
        }

        public async Task<IActionResult> AgregarFactura()
        {
            var token = User.FindFirst("AccessToken")?.Value;
            var response = await _personaService.ObtenerPersonas(token);

            var model = new FacturaFormViewModel
            {
                Personas = response.Data.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nombre
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarFactura(FacturaViewModel facturaViewModel)
        {
            var token = User.FindFirst("AccessToken")?.Value;

            var factura = _mapper.Map<FacturaDTO>(facturaViewModel);

            factura.Fecha = DateTime.Now;

            var response = await _facturaService.AgregarFactura(token, factura);

            return Json(response);
        }
    }
}
