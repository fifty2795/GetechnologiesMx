using AutoMapper;
using Examen.Utilidades.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Examen.Models;
using Examen.Business.Interfaces;
using Examen.Business.DTO;

namespace Examen.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IPersonaService _personaService;
        private readonly IMapper _mapper;

        public PersonaController(IPersonaService personaService, IMapper mapper)
        {
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
            var response = await _personaService.ObtenerPersonas(token, txtBusqueda, pageNumber, pageSize);

            if (!response.Success)
            {
                var modelError = new ResponseViewModel<PaginatedList<PersonaViewModel>>
                {
                    Data = new PaginatedList<PersonaViewModel>(),
                    Success = response.Success,
                    Message = response.Message,
                    Error = response.Error
                };

                return View(modelError);
            }

            var paginatedViewModel = new PaginatedList<PersonaViewModel>(
                        _mapper.Map<List<PersonaViewModel>>(response.Data.Items),
                        response.Data.TotalCount,
                        pageNumber,
                        pageSize);

            var model = new ResponseViewModel<PaginatedList<PersonaViewModel>>
            {
                Success = response.Success,
                Message = response.Message,
                Data = paginatedViewModel,
                Error = response.Error
            };

            return View(model);
        }

        public async Task<IActionResult> AgregarPersona()
        {
            var model = new PersonaViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarPersona(PersonaViewModel persona)
        {
            var token = User.FindFirst("AccessToken")?.Value;

            var personaViewModel = _mapper.Map<PersonaDto>(persona);

            var response = await _personaService.AgregarPersona(token, personaViewModel);

            return Json(response);
        }

        public async Task<IActionResult> EditarPersona(int id)
        {
            var token = User.FindFirst("AccessToken")?.Value;

            var response = await _personaService.ObtenerPersonaById(token, id);

            var model = new PersonaViewModel();

            model.Id = response.Data.Id.Value;
            model.Nombre = response.Data.Nombre;
            model.ApellidoPaterno = response.Data.ApellidoPaterno;
            model.ApellidoMaterno = response.Data.ApellidoMaterno;
            model.Identificacion = response.Data.Identificacion;

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> EditarPersona(PersonaViewModel personaViewModel)
        {
            var token = User.FindFirst("AccessToken")?.Value;

            var persona = _mapper.Map<PersonaDto>(personaViewModel);

            var response = await _personaService.EditarPersona(token, persona);

            return Json(response);
        }

        public async Task<IActionResult> EliminarPersona(int id)
        {
            var token = User.FindFirst("AccessToken")?.Value;
            var response = await _personaService.ObtenerPersonaById(token, id);

            var model = _mapper.Map<PersonaViewModel>(response.Data);

            return View(model);
        }

        public async Task<IActionResult> EliminarPersonaConfirm(int idPersona)
        {
            var token = User.FindFirst("AccessToken")?.Value;
            var response = await _personaService.EliminarPersona(token, idPersona);

            return Json(response);
        }
    }
}
