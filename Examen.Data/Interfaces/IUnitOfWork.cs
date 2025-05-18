using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Data.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<T> Repository<T>() where T : class;

        IPersona RepositorioPersona { get; }

        IFactura RepositorioFactura { get; }        

        Task<int> SaveChangesAsync();
    }
}
