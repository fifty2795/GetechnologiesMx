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
    public class FacturaService: IFacturaService
    {
        private readonly IFacturaServiceAPI _facturaServiceAPI;

        public FacturaService(IFacturaServiceAPI facturaServiceAPI)
        {
            _facturaServiceAPI = facturaServiceAPI;
        }

        public async Task<ResponseViewModel<PaginatedList<FacturaDTO>>> ObtenerFacturas(string token, string txtBusqueda, int pageNumber, int pageSize)
        {
            try
            {
                var result = await _facturaServiceAPI.ObtenerFacturas(token, txtBusqueda);

                if (result == null)
                {
                    return ResponseHelper.CrearRespuestaError<PaginatedList<FacturaDTO>>("Hubo un error con el servicio.");
                }

                var data = result.Data ?? new List<FacturaDTO>();

                var facturasPaginados = PaginatedList<FacturaDTO>.Create(data, pageNumber, pageSize);

                return ResponseHelper.CrearRespuestaExito(facturasPaginados, "Facturas obtenidos exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.CrearRespuestaError<PaginatedList<FacturaDTO>>("Hubo un error con el servicio de persona. Por favor intente de nuevo.");
            }
        }

        public async Task<ResponseViewModel<FacturaDTO>> ObtenerFacturaById(string token, int idFactura, int idPersona)
        {
            try
            {
                var result = await _facturaServiceAPI.ObtenerFacturaById(token, idFactura, idPersona);

                if (!result.Success)
                {
                    return new ResponseViewModel<FacturaDTO>
                    {
                        Success = false,
                        Message = "Factura no encontrado."
                    };
                }

                var data = result.Data ?? new FacturaDTO();

                return ResponseHelper.CrearRespuestaExito(data, "Factura obtenido exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.CrearRespuestaError<FacturaDTO>("Hubo un error con el servicio. Por favor intente de nuevo.");
            }
        }

        public async Task<ResponseViewModel<FacturaDTO>> AgregarFactura(string token, FacturaDTO factura)
        {
            if (factura == null)
            {
                return new ResponseViewModel<FacturaDTO>
                {
                    Success = false,
                    Message = "Factura no puede ser nulo."
                };
            }

            try
            {
                var result = await _facturaServiceAPI.AgregarFactura(token, factura);

                if (!result.Success)
                {
                    return ResponseHelper.CrearRespuestaError<FacturaDTO>("Hubo un error al agregar la factura. Por favor intente de nuevo");
                }

                return ResponseHelper.CrearRespuestaExito(result.Data, "Factura agregado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper.CrearRespuestaError<FacturaDTO>("Hubo un error con el servicio. Por favor intente de nuevo.");
            }
        }
    }
}
