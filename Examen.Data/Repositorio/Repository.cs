using Examen.API.Data.Interfaces;
using Examen.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Data.Repositorio
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ExamenContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ExamenContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> ObtenerQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T> ObtenerByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AgregarAsync(T entidad)
        {
            await _dbSet.AddAsync(entidad);

            return entidad;
        }

        public T Actualizar(T entidad)
        {
            _dbSet.Update(entidad);

            return entidad;
        }

        public async Task<T> EliminarAsync(int id)
        {
            var entidad = await _dbSet.FindAsync(id);

            _dbSet.Remove(entidad);

            return entidad;
        }
    }
}
