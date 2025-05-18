using Examen.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Business.Interfaces
{
    public interface IFacturaServiceAPI
    {
        Task<ResultFacturaListApiDTO?> ObtenerFacturas(string token, string txtBusqueda);

        Task<ResultFacturaApiDTO?> ObtenerFacturaById(string token, int idFactura, int idPersona);

        Task<ResultFacturaApiDTO?> AgregarFactura(string token, FacturaDTO factura);
    }
}
