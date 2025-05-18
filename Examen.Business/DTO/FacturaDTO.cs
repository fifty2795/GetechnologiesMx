using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Business.DTO
{
    public class FacturaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int IdPersona { get; set; }
        public PersonaDTO? Persona { get; set; }
    }

    public class PersonaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public int Identificacion { get; set; }
    }

    public class ResultFacturaApiDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public FacturaDTO? Data { get; set; }
        public int Code { get; set; }
    }

    public class ResultFacturaListApiDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<FacturaDTO> Data { get; set; } = new();
        public int Code { get; set; }
    }

}
