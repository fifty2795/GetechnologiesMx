using Examen.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Business.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(TblPersona usuario);

        ClaimsPrincipal? ValidateToken(string token);
    }
}
