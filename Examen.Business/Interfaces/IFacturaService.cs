using Examen.Business.DTO;
using Examen.Utilidades.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Business.Interfaces
{
    public interface IFacturaService
    {
        Task<ResponseViewModel<PaginatedList<FacturaDTO>>> ObtenerFacturas(string token, string txtBusqueda, int pageNumber, int pageSize);

        Task<ResponseViewModel<FacturaDTO>> ObtenerFacturaById(string token, int idFactura, int idPersona);

        Task<ResponseViewModel<FacturaDTO>> AgregarFactura(string token, FacturaDTO factura);
    }
}
