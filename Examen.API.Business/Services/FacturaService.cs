using AutoMapper;
using Examen.API.Business.DTO;
using Examen.API.Business.Interfaces;
using Examen.API.Data.Interfaces;
using Examen.API.Data.Models;
using Examen.API.Utilidades.Interfaces;
using Examen.API.Utilidades.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Business.Services
{
    public class FacturaService: IFacturaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<TblFactura> _repositorio;
        private readonly ILogService _logService;

        public FacturaService(IUnitOfWork unitOfWork, IMapper mapper, ILogService logService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper;
            _repositorio = _unitOfWork.Repository<TblFactura>();
            _logService = logService;
        }

        public async Task<ResponseViewModel<List<FacturaDTO>>> ObtenerFactura(string idFactura)
        {
            try
            {
                var query = _repositorio.ObtenerQueryable();

                if (!string.IsNullOrEmpty(idFactura))
                {
                    int intIdFactura = Convert.ToInt32(idFactura);
                    query = query.Where(c => c.Id == intIdFactura);
                }

                query = query.Include(c => c.IdPersonaNavigation);

                var facturas = await query.ToListAsync();

                var facturaEntity = _mapper.Map<List<FacturaDTO>>(facturas);

                return ResponseHelper.CrearRespuestaExito(facturaEntity, "Facturas obtenido exitosamente.");
            }
            catch (Exception ex)
            {
                _logService.LogError("Ocurrio un error en ObtenerFactura", ex);
                return ResponseHelper.CrearRespuestaError<List<FacturaDTO>>("Error al obtener las facturas.", 500);
            }
        }

        public async Task<ResponseViewModel<FacturaDTO>> ObtenerFacturaByIdFactIdPersona(int idFactura, int idPersona)
        {
            try
            {
                var factura = await _unitOfWork.RepositorioFactura.ObtenerFacturaByIdFactIdPersona(idFactura, idPersona);

                if (factura == null)
                {
                    return ResponseHelper.CrearRespuestaError<FacturaDTO>("No se encontro ninguna factura.", 401);
                }

                var facturaEntity = _mapper.Map<FacturaDTO>(factura);

                return ResponseHelper.CrearRespuestaExito(facturaEntity, "Factura obtenido exitosamente.");
            }
            catch (Exception ex)
            {
                _logService.LogError("Ocurrio un error en ObtenerPersonaById", ex);
                return ResponseHelper.CrearRespuestaError<FacturaDTO>("Error al obtener la factura.", 500);
            }
        }

        public async Task<ResponseViewModel<FacturaDTO>> AgregarFactura(TblFactura factura)
        {
            try
            {
                if (factura == null)
                {
                    return ResponseHelper.CrearRespuestaError<FacturaDTO>("Favor de ingresar la informacion de la factura.", 401);
                }

                await _repositorio.AgregarAsync(factura);

                await _unitOfWork.SaveChangesAsync();

                var facturaAgregado = await _repositorio.ObtenerByIdAsync(factura.Id);

                var facturaEntity = _mapper.Map<FacturaDTO>(facturaAgregado);

                return ResponseHelper.CrearRespuestaExito(facturaEntity, "Factura agregado exitosamente.");
            }
            catch (Exception ex)
            {
                _logService.LogError("Ocurrio un error en AgregarFactura", ex);
                return ResponseHelper.CrearRespuestaError<FacturaDTO>("Error al agregar la factura.", 500);
            }
        }
    }
}
