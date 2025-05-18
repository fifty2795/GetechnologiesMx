using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Utilidades.Helpers
{
    public static class ResponseHelper
    {
        public static ResponseViewModel<T> CrearRespuestaExito<T>(T? data, string mensajeExito)
        {
            var response = new ResponseViewModel<T>
            {
                Success = true,
                Message = mensajeExito,
                Data = data
            };

            return response;
        }

        public static ResponseViewModel<T> CrearRespuestaError<T>(string mensajeError)
        {
            return new ResponseViewModel<T>
            {
                Success = false,
                Message = mensajeError                
            };
        }
    }
}
