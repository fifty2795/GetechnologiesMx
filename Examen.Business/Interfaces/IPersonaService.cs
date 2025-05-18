using Examen.Business.DTO;
using Examen.Utilidades.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Business.Interfaces
{
    public interface IPersonaService
    {
        Task<ResponseViewModel<PaginatedList<PersonaDto>>> ObtenerPersonas(string token, string? busqueda, int pageNumber, int pageSize);

        Task<ResponseViewModel<List<PersonaDto>>> ObtenerPersonas(string token);

        Task<ResponseViewModel<PersonaDto>> ObtenerPersonaById(string token, int idPersona);

        Task<ResponseViewModel<PersonaDto>> AgregarPersona(string token, PersonaDto persona);

        Task<ResponseViewModel<PersonaDto>> EditarPersona(string token, PersonaDto persona);

        Task<ResponseViewModel<ResultEliminarPersonaApiDTO>> EliminarPersona(string token, int idPersona);
    }
}
