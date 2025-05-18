using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Business.Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int Code { get; set; }

        public LoginData Data { get; set; }
    }

    public class LoginData
    {
        public int Id { get; set; }        

        public string Nombre { get; set; } = string.Empty;

        public string ApellidoPaterno { get; set; } = string.Empty;

        public string ApellidoMaterno { get; set; } = string.Empty;

        public int Identificacion { get; set; }

        public string Token { get; set; } = string.Empty;        
    }
}
