using AutoMapper;
using Examen.API.Business.DTO;
using Examen.API.Business.Interfaces;
using Examen.API.Data.Interfaces;
using Examen.API.Data.Models;
using Examen.API.Utilidades.Interfaces;
using Examen.API.Utilidades.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Business.Services
{
    public class Persona_Service: IPersonaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<TblPersona> _repositorio;
        private readonly ILogService _logService;

        public Persona_Service(IUnitOfWork unitOfWork, IMapper mapper, ILogService logService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper;
            _repositorio = _unitOfWork.Repository<TblPersona>();
            _logService = logService;
        }

        public async Task<ResponseViewModel<List<PersonaDTO>>> ObtenerPersona(string? busqueda)
        {
            try
            {
                var persona = await _unitOfWork.RepositorioPersona.ObtenerPersonas(busqueda);

                if (persona == null)
                {
                    return ResponseHelper.CrearRespuestaError<List<PersonaDTO>>("No se encontro ninguna persona.", 401);
                }

                var personaEntity = _mapper.Map<List<PersonaDTO>>(persona);

                return ResponseHelper.CrearRespuestaExito(personaEntity, "Persona obtenido exitosamente.");
            }
            catch (Exception ex)
            {
                _logService.LogError("Ocurrio un error en ObtenerPersona", ex);
                return ResponseHelper.CrearRespuestaError<List<PersonaDTO>>("Error al obtener a la persona.", 500);
            }
        }

        public async Task<ResponseViewModel<PersonaDTO>> ObtenerPersonaById(int idPersona)
        {
            try
            {
                if (idPersona == 0)
                {
                    return ResponseHelper.CrearRespuestaError<PersonaDTO>("Ingrese un ID de Persona.", 400);
                }

                var persona = await _repositorio.ObtenerByIdAsync(idPersona);

                if (persona == null)
                {
                    return ResponseHelper.CrearRespuestaError<PersonaDTO>("No se encontro ninguna persona.", 401);
                }

                var personaEntity = _mapper.Map<PersonaDTO>(persona);

                return ResponseHelper.CrearRespuestaExito(personaEntity, "Persona obtenido exitosamente.");
            }
            catch (Exception ex)
            {
                _logService.LogError("Ocurrio un error en ObtenerPersonaById", ex);
                return ResponseHelper.CrearRespuestaError<PersonaDTO>("Error al obtener la persona.", 500);
            }
        }

        public async Task<ResponseViewModel<PersonaDTO>> AgregarPersona(TblPersona persona)
        {
            try
            {
                if (persona == null)
                {
                    return ResponseHelper.CrearRespuestaError<PersonaDTO>("Favor de ingresar la informacion de la persona.", 401);
                }                

                await _repositorio.AgregarAsync(persona);

                await _unitOfWork.SaveChangesAsync();

                var personaAgregado = await _repositorio.ObtenerByIdAsync(persona.Id);

                var personaEntity = _mapper.Map<PersonaDTO>(personaAgregado);

                return ResponseHelper.CrearRespuestaExito(personaEntity, "Persona agregado exitosamente.");
            }
            catch (Exception ex)
            {
                _logService.LogError("Ocurrio un error en AgregarPersona", ex);
                return ResponseHelper.CrearRespuestaError<PersonaDTO>("Error al agregar la persona.", 500);
            }
        }

        public async Task<ResponseViewModel<PersonaDTO>> EditarPersona(TblPersona persona)
        {
            try
            {
                if (persona == null)
                {
                    return ResponseHelper.CrearRespuestaError<PersonaDTO>("Favor de ingresar la informacion de la persona.", 401);
                }

                await _unitOfWork.RepositorioPersona.ActualizarPersona(persona);

                var personaActualizado = await _repositorio.ObtenerByIdAsync(persona.Id);

                var personaEntity = _mapper.Map<PersonaDTO>(personaActualizado);

                return ResponseHelper.CrearRespuestaExito(personaEntity, "Persona actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                _logService.LogError("Ocurrio un error en EditarPersona", ex);
                return ResponseHelper.CrearRespuestaError<PersonaDTO>("Error al actualizar la persona.", 500);
            }
        }

        public async Task<ResponseViewModel<bool>> EliminarPersona(int idPersona)
        {
            try
            {
                await _unitOfWork.RepositorioFactura.EliminarFacturas(idPersona);

                await _repositorio.EliminarAsync(idPersona);

                await _unitOfWork.SaveChangesAsync();

                return ResponseHelper.CrearRespuestaExito(true, "Persona eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                _logService.LogError("Ocurrio un error en EliminarPersona", ex);
                return ResponseHelper.CrearRespuestaError<bool>("Error al eliminar la persona.", 500);
            }
        }
    }
}
