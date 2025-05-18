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
    public class Repositorio_Persona: IPersona
    {
        private readonly ExamenContext _dbContext;
        private readonly ILogService _logService;

        public Repositorio_Persona(ExamenContext dbContext, ILogService logService)
        {
            _dbContext = dbContext;
            _logService = logService;
        }

        public async Task<TblPersona?> ObtenerPersonaByCredenciales(string nombre, int identificacion)
        {
            return await _dbContext.TblPersonas.Where(x => x.Nombre == nombre && x.Identificacion == identificacion).FirstOrDefaultAsync();
        }

        public async Task<List<TblPersona?>> ObtenerPersonas(string? busqueda)
        {
            if (string.IsNullOrEmpty(busqueda))
            {
                return await _dbContext.TblPersonas.ToListAsync();
            }
            return await _dbContext.TblPersonas.Where(x => x.Nombre.Contains(busqueda) || x.ApellidoPaterno.Contains(busqueda) || x.ApellidoMaterno.Contains(busqueda)).ToListAsync();
        }

        public async Task ActualizarPersona(TblPersona persona)
        {
            try
            {
                var personaOriginal = await _dbContext.TblPersonas.FirstOrDefaultAsync(p => p.Id == persona.Id);

                if (personaOriginal == null)
                    throw new Exception("Persona no encontrado");

                personaOriginal.Nombre = persona.Nombre;
                personaOriginal.ApellidoPaterno = persona.ApellidoPaterno;
                personaOriginal.ApellidoMaterno = persona.ApellidoMaterno;
                personaOriginal.Identificacion = persona.Identificacion;                

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logService.LogError("Ocurrio un error en Repositorio Persona: ActualizarPersona", ex);
                throw;
            }
        }
    }
}
