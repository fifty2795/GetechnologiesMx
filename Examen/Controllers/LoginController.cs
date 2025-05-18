using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Examen.Business.Interfaces;

namespace Examen.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IPersonaServiceAPI _personaServiceAPI;
        private readonly IWebHostEnvironment _webHostEnvironment;        
        private readonly IMapper _mapper;

        public LoginController(IPersonaServiceAPI personaServiceAPI, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _personaServiceAPI = personaServiceAPI;
            _webHostEnvironment = webHostEnvironment;            
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }      

        [HttpPost]
        public async Task<IActionResult> Login(string nombre, int identificacion)
        {
            if (!string.IsNullOrEmpty(nombre))
            {
                var usuario = await _personaServiceAPI.ObtenerTokenAsync(nombre, identificacion);

                if (usuario != null)
                {
                    var claims = new List<Claim>
                        {
                            // Información básica del usuario
                            new Claim(ClaimTypes.Name, usuario.Data.Nombre + ' ' + usuario.Data.ApellidoPaterno),                            

                             // Claims adicionales
                            new Claim("ID", usuario.Data.Id.ToString()),                            
                            new Claim("AccessToken", usuario.Data.Token),                            
                        };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
                }
            }
            return Json(new { success = false, message = "Usuario o contraseña incorrectos." });
        }

        public async Task<IActionResult> Logout()
        {
            int idUsuario = Convert.ToInt32(User.FindFirst("IdUsuario")?.Value);
            var IsAuthenticated = User.Identity.IsAuthenticated;

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (IsAuthenticated)
            {
                // Elimina todas las cookies posibles
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }                
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
