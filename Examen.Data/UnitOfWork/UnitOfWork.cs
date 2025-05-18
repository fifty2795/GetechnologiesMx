using Examen.API.Data.Interfaces;
using Examen.API.Data.Models;
using Examen.API.Data.Repositorio;
using Examen.API.Utilidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Data.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ExamenContext _context;
        private readonly Dictionary<Type, object> _repositories = new();

        private readonly ILogService _logService;

        public IPersona RepositorioPersona { get; private set; }
        public IFactura RepositorioFactura { get; private set; }        

        public UnitOfWork(ExamenContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;

            RepositorioPersona = new Repositorio_Persona(_context, _logService);
            RepositorioFactura = new Repositorio_Factura(_context, _logService);            
        }

        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new Repository<T>(_context);
                _repositories[type] = repositoryInstance;
            }

            return (IRepository<T>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
