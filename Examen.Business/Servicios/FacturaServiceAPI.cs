using Examen.API.Data.Interfaces;
using Examen.Business.DTO;
using Examen.Business.Interfaces;
using Examen.Business.Models;
using Examen.Business.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Business.Servicios
{
    public class FacturaServiceAPI: IFacturaServiceAPI
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public FacturaServiceAPI(HttpClient httpClient, IOptions<ApiSettings> options)
        {
            _httpClient = httpClient;
            _baseUrl = options.Value.BaseUrl;
        }        

        public async Task<ResultFacturaListApiDTO?> ObtenerFacturas(string token, string txtBusqueda)
        {            
            string url = $"{_baseUrl}/Factura/obtenerFacturas?idFactura={txtBusqueda}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ResultFacturaListApiDTO>();

            return null;
        }

        public async Task<ResultFacturaApiDTO?> ObtenerFacturaById(string token, int idFactura, int idPersona)
        {
            string url = $"{_baseUrl}/Factura/obtenerFacturaById?idFactura={idPersona}&idPersona={idPersona}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ResultFacturaApiDTO>();

            return null;
        }

        public async Task<ResultFacturaApiDTO?> AgregarFactura(string token, FacturaDTO factura)
        {
            string url = $"{_baseUrl}/Factura/agregarFactura";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync(url, factura);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ResultFacturaApiDTO>();

            return null;
        }
    }
}
