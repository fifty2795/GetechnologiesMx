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
    public interface IFacturaService
    {
        Task<ResponseViewModel<List<FacturaDTO>>> ObtenerFactura(string idFactura);

        Task<ResponseViewModel<FacturaDTO>> ObtenerFacturaByIdFactIdPersona(int idFactura, int idPersona);

        Task<ResponseViewModel<FacturaDTO>> AgregarFactura(TblFactura factura);
    }
}
