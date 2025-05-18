using Examen.Business.DTO;
using Examen.Business.Interfaces;
using Examen.Utilidades.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Business.Servicios
{
    public class PersonaService: IPersonaService
    {
        private readonly IPersonaServiceAPI _personaServiceAPI;        

        public PersonaService(IPersonaServiceAPI personaServiceAPI)
        {
            _personaServiceAPI = personaServiceAPI;            
        }

        public async Task<ResponseViewModel<PaginatedList<PersonaDto>>> ObtenerPersonas(string token, string? busqueda, int pageNumber, int pageSize)
        {
            try
            {
                var result = await _personaServiceAPI.ObtenerPersona(token, busqueda);

                if (result == null)
                {
                    return ResponseHelper.CrearRespuestaError<PaginatedList<PersonaDto>>("Hubo un error con el servicio de persona.");
                }

                var data = result.Data ?? new List<PersonaDto>();

                var productosPaginados = PaginatedList<PersonaDto>.Create(data, pageNumber, pageSize);

                return ResponseHelper.CrearRespuestaExito(productosPaginados, "Personas obtenidos exitosamente.");
            }
            catch (Exception ex)
            {                
                return ResponseHelper.CrearRespuestaError<PaginatedList<PersonaDto>>("Hubo un error con el servicio de persona. Por favor intente de nuevo.");
            }
        }

        public async Task<ResponseViewModel<List<PersonaDto>>> ObtenerPersonas(string token)
        {
            try
            {
                var result = await _personaServiceAPI.ObtenerPersona(token, null);

                if (result == null)
                {
                    return ResponseHelper.CrearRespuestaError<List<PersonaDto>>("Hubo un error con el servicio.");
                }

                var data = result.Data ?? new List<PersonaDto>();

                return ResponseHelper.CrearRespuestaExito(data, "Personas obtenidos exitosamente.");
            }
            catch (Exception ex)
            {                
                return ResponseHelper.CrearRespuestaError<List<PersonaDto>>("Hubo un error con el servicio. Por favor intente de nuevo.");
            }
        }

        public async Task<ResponseViewModel<PersonaDto>> ObtenerPersonaById(string token, int idPersona)
        {
            try
            {
                var result = await _personaServiceAPI.ObtenerPersonaById(token, idPersona);

                if (!result.Success)
                {
                    return new ResponseViewModel<PersonaDto>
                    {
                        Success = false,
                        Message = "Persona no encontrado."
                    };
                }

                var data = result.Data ?? new PersonaDto();

                return ResponseHelper.CrearRespuestaExito(data, "Persona obtenido exitosamente.");
            }
            catch (Exception ex)
            {                
                return ResponseHelper.CrearRespuestaError<PersonaDto>("Hubo un error con el servicio. Por favor intente de nuevo.");
            }
        }

        public async Task<ResponseViewModel<PersonaDto>> AgregarPersona(string token, PersonaDto persona)
        {
            if (persona == null)
            {
                return new ResponseViewModel<PersonaDto>
                {
                    Success = false,
                    Message = "Persona no puede ser nulo."
                };
            }

            try
            {                
                var result = await _personaServiceAPI.AgregarPersona(token, persona);

                if (!result.Success)
                {
                    return ResponseHelper.CrearRespuestaError<PersonaDto>("Hubo un error al agregar la persona. Por favor intente de nuevo");
                }

                return ResponseHelper.CrearRespuestaExito(result.Data, "Persona agregado con éxito");
            }
            catch (Exception ex)
            {                
                return ResponseHelper.CrearRespuestaError<PersonaDto>("Hubo un error con el servicio. Por favor intente de nuevo.");
            }
        }

        public async Task<ResponseViewModel<PersonaDto>> EditarPersona(string token, PersonaDto persona)
        {
            if (persona == null)
            {
                return new ResponseViewModel<PersonaDto>
                {
                    Success = false,
                    Message = "Persona no puede ser nulo."
                };
            }

            try
            {
                var result = await _personaServiceAPI.EditarPersona(token, persona);

                if (result == null)
                {
                    return ResponseHelper.CrearRespuestaError<PersonaDto>("Hubo un error al actualizar la persona. Por favor intente de nuevo");
                }

                var data = result.Data ?? new PersonaDto();

                return ResponseHelper.CrearRespuestaExito(data, "Persona actualizado con éxito");
            }
            catch (Exception ex)
            {                
                return ResponseHelper.CrearRespuestaError<PersonaDto>("Hubo un error con el servicio. Por favor intente de nuevo.");
            }
        }

        public async Task<ResponseViewModel<ResultEliminarPersonaApiDTO>> EliminarPersona(string token, int idPersona)
        {
            try
            {
                var result = await _personaServiceAPI.EliminarPersona(token, idPersona);

                if (!result.Success)
                {
                    return new ResponseViewModel<ResultEliminarPersonaApiDTO>
                    {
                        Success = false,
                        Message = "No se pudo eliminar la persona, no existe."
                    };
                }

                return ResponseHelper.CrearRespuestaExito(result, "Persona eliminado con éxito");
            }
            catch (Exception ex)
            {                
                return ResponseHelper.CrearRespuestaError<ResultEliminarPersonaApiDTO>("Hubo un error con el servicio. Por favor intente de nuevo.");
            }
        }        
    }
}
