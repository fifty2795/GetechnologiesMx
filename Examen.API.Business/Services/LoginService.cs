using Examen.API.Business.Interfaces;
using Examen.API.Business.Settings;
using Examen.API.Data.Interfaces;
using Examen.API.Data.Models;
using Microsoft.Extensions.Options;
using AutoMapper;
using Examen.API.Utilidades.Interfaces;
using Examen.API.Utilidades.Response;
using Examen.API.Business.DTO;

namespace Examen.API.Business.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;        
        private readonly IMapper _mapper;                
        private readonly IJwtService _jwtService;

        public LoginService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtOptions, IMapper mapper, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper;            
            _jwtService = jwtService;
        }

        public async Task<ResponseViewModel<PersonaDTO>> Login(LoginDTO request)
        {
            var persona = await _unitOfWork.RepositorioPersona.ObtenerPersonaByCredenciales(request.Nombre, request.Identificacion);

            if (persona == null)
                return ResponseHelper.CrearRespuestaError<PersonaDTO>("Credenciales inválidas.", 401);

            persona.Token = _jwtService.GenerateToken(persona);

            var personaDto = _mapper.Map<PersonaDTO>(persona);

            return ResponseHelper.CrearRespuestaExito(personaDto, "Usuario autenticado exitosamente.");
        }
    }
}
