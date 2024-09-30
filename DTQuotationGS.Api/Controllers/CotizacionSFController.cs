using DTQuotationGS.Business;
using DTQuotationGS.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace DTQuotationGS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CotizacionSFController : ControllerBase
    {

        private readonly CotizacionSFBusiness _service;

        public CotizacionSFController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("tijuana");
            _service = new CotizacionSFBusiness(connectionString);
        }


        //Endpoint para obtener todas las facturas en un intervalo de minutos
       [HttpGet("SelectQuo")]
       [SwaggerResponse(200, "Success", typeof(List<CotizacionSFEntity>))]
       [SwaggerResponse(404, "Not Found")]
        public IActionResult GetFacturas([FromQuery] int intervaloMinutos)
        {
            try
            {
                var entities = _service.GetCotizacionSF(intervaloMinutos);
                if (entities == null || !entities.Any())
                {
                    return NotFound();
                }
                return Ok(entities);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones o logging
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }


        ////[HttpPost("InsertQuo")]
        ////[SwaggerResponse(200, "Success", typeof(List<CotizacionSFViewModel>))]
        ////[SwaggerResponse(404, "Not Found")]
        ////public IActionResult InsertQuotationSF([FromBody] List<CotizacionSFViewModel> cotizaciones)
        ////{
        ////    try
        ////    {
        ////        var entity = _service.InsertQuotationSF(cotizaciones);
        ////        if (entity == null)
        ////        {
        ////            return NotFound();
        ////        }
        ////        return Ok(entity);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        // Manejo de excepciones o logging
        ////        return StatusCode(500, "Error interno del servidor: " + ex.Message);
        ////    }
        ////}

        //private readonly IConfiguration _configuration;

        //public CotizacionSFController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

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


    }
}
