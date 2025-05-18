using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Business.DTO
{
    public class PersonaDto
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }

        public int Identificacion { get; set; }
    }

    public class ResultPersonaApiDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public PersonaDto? Data { get; set; }
        public int Code { get; set; }
    }

    public class ResultPersonaListApiDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<PersonaDto> Data { get; set; } = new();
        public int Code { get; set; }
    }

    public class ResultEliminarPersonaApiDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Data { get; set; }
        public int Code { get; set; }
    }
}
