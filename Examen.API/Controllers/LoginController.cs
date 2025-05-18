using AutoMapper;
using Examen.API.Business.DTO;
using Examen.API.Business.Interfaces;
using Examen.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;

        public LoginController(ILoginService loginService, IMapper mapper)
        {
            _loginService = loginService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginDto = _mapper.Map<LoginDTO>(model);

            var result = await _loginService.Login(loginDto);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}
