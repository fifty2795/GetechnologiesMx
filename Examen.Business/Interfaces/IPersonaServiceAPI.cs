using Examen.Business.DTO;
using Examen.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Business.Interfaces
{
    public interface IPersonaServiceAPI
    {
        Task<LoginResponse?> ObtenerTokenAsync(string nombre, int identificacion);

        Task<ResultPersonaListApiDTO?> ObtenerPersona(string token, string? busqueda);

        Task<ResultPersonaApiDTO?> ObtenerPersonaById(string token, int idPersona);

        Task<ResultPersonaApiDTO?> AgregarPersona(string token, PersonaDto persona);

        Task<ResultPersonaApiDTO?> EditarPersona(string token, PersonaDto persona);

        Task<ResultEliminarPersonaApiDTO?> EliminarPersona(string token, int idPersona);
    }
}
