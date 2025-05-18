using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Business.DTO
{
    public class LoginDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public int Identificacion { get; set; }
    }
}
