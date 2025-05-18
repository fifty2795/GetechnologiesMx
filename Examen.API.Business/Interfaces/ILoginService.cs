using Examen.API.Business.DTO;
using Examen.API.Utilidades.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Business.Interfaces
{
    public interface ILoginService
    {
        Task<ResponseViewModel<PersonaDTO>> Login(LoginDTO request);
    }
}
