using Examen.API.Data.Interfaces;
using Examen.API.Data.Models;
using Examen.API.Utilidades.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Data.Repositorio
{
    public class Repositorio_Factura: IFactura
    {
        private readonly ExamenContext _dbContext;
        private readonly ILogService _logService;

        public Repositorio_Factura(ExamenContext dbContext, ILogService logService)
        {
            _dbContext = dbContext;
            _logService = logService;
        }

        public async Task<TblFactura?> ObtenerFacturaByIdFactIdPersona(int? idFactura, int? idPersona)
        {
            var query = _dbContext.TblFacturas.AsQueryable();

            if (idFactura.HasValue)
                query = query.Where(x => x.Id == idFactura.Value);

            if (idPersona.HasValue)
                query = query.Where(x => x.IdPersona == idPersona.Value);

            return await query.FirstOrDefaultAsync();
        }

        public async Task EliminarFacturas(int idPersona)
        {
            try
            {
                var facturas = _dbContext.TblFacturas.Where(f => f.IdPersona == idPersona);

                if (facturas.Any())
                {
                    _dbContext.TblFacturas.RemoveRange(facturas);
                    await _dbContext.SaveChangesAsync();                    
                }                
            }
            catch (Exception ex)
            {
                _logService.LogError("Ocurrio un error en EliminarFacturas", ex);
            }
        }
    }
}
