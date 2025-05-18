using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.API.Business.DTO
{
    public class PersonaDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string ApellidoPaterno { get; set; } = null!;

        public string? ApellidoMaterno { get; set; }

        public int Identificacion { get; set; }

        public string? Token { get; set; }
    }
}
