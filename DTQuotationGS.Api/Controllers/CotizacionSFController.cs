using DTQuotationGS.Business;
using DTQuotationGS.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System.Configuration;

namespace DTQuotationGS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CotizacionSFController : ControllerBase
    {
        private readonly CotizacionSFBusiness _service;

        //public CotizacionSFController(IConfiguration configuration, string ub)
        //{
        //    var connectionString = configuration.GetConnectionString("LogDatabase");

        //    var connectionUrls = configuration.GetSection("ApiSettings:Apis").GetValue<string>(ub);
        //    _service = new CotizacionSFBusiness(connectionString, connectionUrls);
        //}


        // //Endpoint para obtener todas las facturas en un intervalo de minutos
        //[HttpGet("SelectQuo")]
        //[SwaggerResponse(200, "Success", typeof(List<CotizacionSFEntity>))]
        //[SwaggerResponse(404, "Not Found")]
        // public IActionResult GetFacturas([FromQuery] int intervaloMinutos)
        // {
        //     try
        //     {
        //         var entities = _service.GetCotizacionSF(intervaloMinutos);
        //         if (entities == null || !entities.Any())
        //         {
        //             return NotFound();
        //         }
        //         return Ok(entities);
        //     }
        //     catch (Exception ex)
        //     {
        //         // Manejo de excepciones o logging
        //         return StatusCode(500, "Error interno del servidor: " + ex.Message);
        //     }
        // }


        //[HttpPost("InsertQuo")]
        //[SwaggerResponse(200, "Success", typeof(List<CotizacionSFViewModel>))]
        //[SwaggerResponse(404, "Not Found")]
        //public IActionResult InsertQuotationSF([FromBody] List<CotizacionSFViewModel> cotizaciones)
        //{
        //    try
        //    {
        //        var entity = _service.InsertQuotationSF(cotizaciones);
        //        if (entity == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones o logging
        //        return StatusCode(500, "Error interno del servidor: " + ex.Message);
        //    }
        //}

        private readonly IConfiguration _configuration;

        public CotizacionSFController(IConfiguration configuration)
        {
            //    var connectionString = configuration.GetConnectionString("LogDatabase");

            //    //    var connectionUrls = configuration.GetSection("ApiSettings:Apis").GetValue<string>(ub);
            //    _service = new CotizacionSFBusiness(connectionString);

            _configuration = configuration;
        }

        //[HttpPost("InsertQuo")]
        //[SwaggerResponse(200, "Success", typeof(List<CotizacionSFViewModel>))]
        //[SwaggerResponse(404, "Not Found")]
        //public IActionResult InsertQuotationSF([FromBody] List<CotizacionSFViewModel> cotizaciones)
        //{
        //    try
        //    {
        //        if (cotizaciones == null || !cotizaciones.Any())
        //        {
        //            return BadRequest("No se enviaron cotizaciones.");
        //        }

        //        // Obtén la ubicación de la primera cotización
        //        var ub = cotizaciones.First().Ubicacion; // Asegúrate que 'Ubicacion' es el campo correcto

        //        // Obtener el nombre de la cadena de conexión desde la configuración
        //        var connectionMap = _configuration.GetSection("UbicacionConnectionMap").Get<Dictionary<string, string>>();

        //        if (connectionMap.TryGetValue(ub, out string connectionKey))
        //        {
        //            // Si la ubicación existe en el mapeo, obtener la cadena de conexión
        //            var connectionString = _configuration.GetConnectionString(connectionKey);

        //            // Crear la instancia de negocio con la conexión seleccionada
        //            var _service = new CotizacionSFBusiness(connectionString);


        //            var entity = _service.InsertQuotationSF(cotizaciones);
        //            if (entity == null)
        //            {
        //                return NotFound();
        //            }
        //            return Ok(entity);
        //        }
        //        else
        //        {
        //            return BadRequest($"Ubicación '{ub}' no tiene una cadena de conexión configurada.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones o logging
        //        return StatusCode(500, "Error interno del servidor: " + ex.Message);
        //    }
        //}




        //private readonly CotizacionSFBusiness _service;
        //private readonly HttpClient _httpClient;
        //private readonly IConfiguration _configuration;
        //private readonly Dictionary<string, string> _apiUrls;
        //private readonly ILogger<CotizacionSFController> _logger;


        //public CotizacionSFController(IConfiguration configuration, HttpClient httpClient, CotizacionSFBusiness service, ILogger<CotizacionSFController> logger)
        //{

        //    _service = service;
        //    _httpClient = httpClient;
        //    _configuration = configuration;

        //    _apiUrls = _configuration.GetSection("Apis").Get<Dictionary<string, string>>();
        //    _logger = logger;

        //    //var connectionString = configuration.GetConnectionString("tijuana");
        //    //_service = new CotizacionSFBusiness(connectionString);
        //    //_httpClient = httpClient;
        //}


        //[HttpPost("InsertQuo")]
        //[SwaggerResponse(200, "Success", typeof(List<CotizacionSFViewModel>))]
        //[SwaggerResponse(404, "Not Found")]
        //public async Task<IActionResult> InsertQuotationSFAsync([FromBody] List<CotizacionSFViewModel> cotizaciones)
        //{

        //    try
        //    {
        //        var entity = _service.InsertQuotationSF(cotizaciones);
        //        if (entity == null)
        //        {
        //            return NotFound();
        //        }
        //        string apiUrl = DetermineApiUrl(cotizaciones);

        //        // Enviar los datos a la API seleccionada
        //        var response = await _httpClient.PostAsJsonAsync(apiUrl, entity);
        //        if (!response.IsSuccessStatusCode)
        //        {
        //            // Manejo de errores si la llamada a la API falla
        //            return StatusCode((int)response.StatusCode, "Error al enviar datos a la otra API.");
        //        }

        //        return Ok(entity);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        _logger.LogWarning(ex, "NotFoundException in GenerateInvoice for ID {Id}", cotizaciones);
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Exception in GenerateInvoice for ID {Id}", cotizaciones);
        //        //await _logger.Log(ex, $"{nameof(InvoiceController)}_{nameof(GenerateInvoice)}");
        //        return BadRequest(ex.Message);
        //    }
        //}

        //private string DetermineApiUrl(List<CotizacionSFViewModel> cotizaciones)
        //{
        //    // Suponiendo que en la lista hay un criterio para seleccionar la API.
        //    // Ejemplo: basado en el QuoClient
        //    string clientKey = cotizaciones.FirstOrDefault()?.Ubicacion; // Cambia esto según tu criterio

        //    // Verificar si el cliente está en el diccionario
        //    if (clientKey != null && _apiUrls.TryGetValue(clientKey, out var apiUrl))
        //    {
        //        return apiUrl;
        //    }

        //    // Si no se encuentra un cliente válido, puedes lanzar una excepción o manejarlo como prefieras
        //    throw new InvalidOperationException("No se encontró una API para el cliente especificado.");
        //}




        [HttpPost("InsertQuo")]
        [SwaggerResponse(200, "Success", typeof(List<CotizacionSFViewModel>))]
        [SwaggerResponse(404, "Not Found")]
        public IActionResult InsertQuotationSF([FromBody] List<CotizacionSFViewModel> cotizaciones)
        {
            try
            {
                if (cotizaciones == null || !cotizaciones.Any())
                {
                    return BadRequest("No se enviaron cotizaciones.");
                }

                var connectionString = _configuration.GetConnectionString("LogDatabase");

                // Obtén la ubicación de la primera cotización
                var ub = cotizaciones.First().Ubicacion; // Asegúrate que 'Ubicacion' es el campo correcto
                var connectionMap = _configuration.GetSection("ApiSettings:Apis").Get<Dictionary<string, string>>();

                if (connectionMap.TryGetValue(ub, out string connectionKey))
                {
                    // Si la ubicación existe en el mapeo, obtener la cadena de conexión
                    var connectionUrls = (connectionKey);

                    // Crear la instancia de negocio con la conexión seleccionada
                    var _service = new CotizacionSFBusiness(connectionString,connectionUrls);

                    var entity = _service.InsertQuotationSF(cotizaciones);
                    if (entity == null)
                    {
                        return NotFound();
                    }
                    return Ok(entity);
                }

                else
                        {
                    return BadRequest($"Ubicación '{ub}' no tiene una cadena de conexión configurada.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones o logging
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

    }
}
