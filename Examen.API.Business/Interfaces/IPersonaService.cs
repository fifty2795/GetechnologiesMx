using Examen.API.Business.DTO;
using Examen.API.Data.Models;
using Examen.API.Utilidades.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Business.Interfaces
{
    public interface IPersonaService
    {
        Task<ResponseViewModel<List<PersonaDTO>>> ObtenerPersona(string? busqueda);

        Task<ResponseViewModel<PersonaDTO>> ObtenerPersonaById(int idPersona);

        Task<ResponseViewModel<PersonaDTO>> AgregarPersona(TblPersona persona);

        Task<ResponseViewModel<PersonaDTO>> EditarPersona(TblPersona persona);

        Task<ResponseViewModel<bool>> EliminarPersona(int idPersona);
    }
}
