using Examen.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Data.Interfaces
{
    public interface IFactura
    {
        Task<TblFactura?> ObtenerFacturaByIdFactIdPersona(int? idFactura, int? idPersona);

        Task EliminarFacturas(int idPersona);
    }
}
