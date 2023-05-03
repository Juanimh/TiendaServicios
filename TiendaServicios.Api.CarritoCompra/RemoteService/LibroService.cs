using System.Text.Json;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteService
{
    public class LibroService : ILibroService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        public LibroService(IHttpClientFactory httpClient, ILogger<LibroService> logger)
        {
            _httpClientFactory = httpClient;0
            _logger = logger;
        }

        public async Task<(bool resultad, LibroRemote libro, string errorMensaje)> GetLibro(Guid guidLibro)
        {
            try
            {
                HttpClient librosHttpClient = _httpClientFactory.CreateClient("Libros");
                HttpResponseMessage response = await librosHttpClient.GetAsync($"api/LibroMaterial/{guidLibro}");

                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);

                    return (true, resultado, null);
                }

                return (false, null, response.ReasonPhrase);

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
