using Examen.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Data.Interfaces
{
    public interface IPersona
    {
        Task<TblPersona?> ObtenerPersonaByCredenciales(string nombre, int identificacion);

        Task<List<TblPersona?>> ObtenerPersonas(string? busqueda);

        Task ActualizarPersona(TblPersona persona);
    }
}
