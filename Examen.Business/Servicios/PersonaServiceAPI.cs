using Examen.Business.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Examen.Business.Settings;
using Examen.Business.Models;
using System.Net.Http.Headers;
using Examen.Business.DTO;

namespace Examen.Business.Servicios
{
    public class PersonaServiceAPI: IPersonaServiceAPI
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public PersonaServiceAPI(HttpClient httpClient, IOptions<ApiSettings> options)
        {
            _httpClient = httpClient;
            _baseUrl = options.Value.BaseUrl;
        }

        public async Task<LoginResponse?> ObtenerTokenAsync(string nombre, int identificacion)
        {
            var loginPayload = new { nombre, identificacion };
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Login/Login", loginPayload);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return content;
            }

            return null;
        }

        public async Task<ResultPersonaListApiDTO?> ObtenerPersona(string token, string? busqueda)
        {
            var query = string.IsNullOrWhiteSpace(busqueda) ? "" : $"?Nombre={busqueda}";
            string url = $"{_baseUrl}/Persona/obtenerPersonas{query}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ResultPersonaListApiDTO>();

            return null;
        }

        public async Task<ResultPersonaApiDTO?> ObtenerPersonaById(string token, int idPersona)
        {
            string url = $"{_baseUrl}/Persona/obtenerPersonaById?idPersona={idPersona}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ResultPersonaApiDTO>();

            return null;
        }

        public async Task<ResultPersonaApiDTO?> AgregarPersona(string token, PersonaDto persona)
        {
            string url = $"{_baseUrl}/Persona/agregarPersona";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync(url, persona);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ResultPersonaApiDTO>();

            return null;
        }

        public async Task<ResultPersonaApiDTO?> EditarPersona(string token, PersonaDto persona)
        {
            string url = $"{_baseUrl}/Persona/editarPersona";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync(url, persona);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ResultPersonaApiDTO>();

            return null;
        }

        public async Task<ResultEliminarPersonaApiDTO?> EliminarPersona(string token, int idPersona)
        {
            string url = $"{_baseUrl}/Persona/eliminarPersona?idPersona={idPersona}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ResultEliminarPersonaApiDTO>();

            return null;
        }
    }
}
