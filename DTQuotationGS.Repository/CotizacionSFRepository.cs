using DTQuotationGS.Entities;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DTQuotationGS.Repository
{
    public class CotizacionSFRepository
    {
        private readonly string _connectionString;
        private readonly string _connectionUrls;

        public decimal tdc;

        public CotizacionSFRepository(string connectionString, string connectionUrls)
        {
            _connectionString = connectionString;
            _connectionUrls = connectionUrls;
        }



        public List<CotizacionSFEntity> GetCotizacionSF(int intervaloMinutos)
        {
          var cotizaciones = new List<CotizacionSFEntity>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[STP_Quotation]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@diferencia", intervaloMinutos);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Leer la primera tabla (facturas)
                            while (reader.Read())
                            {
                                var cotizacion = new CotizacionSFEntity
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    IdQuotation = reader["IdQuotation"].ToString(),
                                    QuoClient = reader["QuoClient"].ToString(),
                                    QuoVendor = reader["QuoVendor"].ToString(),
                                    QuoCreated = reader.GetDateTime(reader.GetOrdinal("QuoCreated")),
                                    QuoExpiration = reader.GetDateTime(reader.GetOrdinal("QuoExpiration")),
                                    QuoTotal = reader.GetDecimal(reader.GetOrdinal("QuoTotal")),

                                    QuoCurrency = reader["QuoCurrency"].ToString(),
                                    Send = reader.GetBoolean(reader.GetOrdinal("Send")),
                                    Detalles = new List<CotizacionSFEntityDetalle>() // Inicializa la lista de detalles
                                };

                                // Comprobar si hay más resultados (detalles)
                                if (reader.NextResult())
                                {
                                    while (reader.Read())
                                    {
                                        var detalle = new CotizacionSFEntityDetalle
                                        {
                                            IdQuotationDetails = reader.GetInt32(reader.GetOrdinal("IdQuotationDetails")), 
                                            IdMaster = reader.GetInt32(reader.GetOrdinal("IdMaster")), 
                                            Model = reader["Model"].ToString(),
                                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                            UnitPriceIVA = reader.GetDecimal(reader.GetOrdinal("UnitPriceIVA")),
                                            Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal"))
                                        };

                                        cotizacion.Detalles.Add(detalle);
                                    }
                                }

                                cotizaciones.Add(cotizacion);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores detallado
                        throw new Exception($"Error al obtener las facturas: {ex.Message}", ex);
                    }
                    finally
                    {
                        // No es necesario cerrar la conexión aquí debido a la declaración 'using'
                    }
                }
            }

            return cotizaciones;
        }

        //public List<CotizacionSFViewModel> InsertQuotationSF(List<CotizacionSFViewModel> cotizaciones)
        //{

        //    var insertedCotizaciones = new List<CotizacionSFViewModel>();

        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        conn.Open();
        //        foreach (var cotizacion in cotizaciones)
        //        {
        //            using (SqlCommand cmd = new SqlCommand("[dbo].[STP_InsertQuotation]", conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                // Parámetros de la cotización
        //                cmd.Parameters.AddWithValue("@IdQuotation", cotizacion.IdQuotation);
        //                cmd.Parameters.AddWithValue("@QuoClient", cotizacion.QuoClient);
        //                cmd.Parameters.AddWithValue("@QuoVendor", cotizacion.QuoVendor);
        //                cmd.Parameters.AddWithValue("@QuoCreated", cotizacion.QuoCreated);
        //                cmd.Parameters.AddWithValue("@QuoExpiration", cotizacion.QuoExpiration);
        //                cmd.Parameters.AddWithValue("@QuoTotal", cotizacion.QuoTotal);
        //                cmd.Parameters.AddWithValue("@QuoCurrency", cotizacion.QuoCurrency);

        //                SqlParameter idMParam = new SqlParameter("@Id", SqlDbType.Int)
        //                {
        //                    Direction = ParameterDirection.Output
        //                };
        //                cmd.Parameters.Add(idMParam);

        //                cmd.ExecuteNonQuery(); // Inserta la cotización

        //                int idQuotationGenerated = (int)idMParam.Value;

        //                // Insertar detalles
        //                foreach (var detalle in cotizacion.Detalles)
        //                {
        //                    using (SqlCommand cmdDetalle = new SqlCommand("[dbo].[STP_InsertQuotationDetail]", conn))
        //                    {
        //                        cmdDetalle.CommandType = CommandType.StoredProcedure;

        //                        // Parámetros de los detalles de la cotización
        //                        cmdDetalle.Parameters.AddWithValue("@IdMaster", idQuotationGenerated);
        //                        cmdDetalle.Parameters.AddWithValue("@Model", detalle.Model);
        //                        cmdDetalle.Parameters.AddWithValue("@Quantity", detalle.Quantity);
        //                        cmdDetalle.Parameters.AddWithValue("@UnitPriceIVA", detalle.UnitPriceIVA);
        //                        cmdDetalle.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);

        //                        cmdDetalle.ExecuteNonQuery(); // Inserta cada detalle
        //                    }
        //                }
        //            }

        //            insertedCotizaciones.Add(cotizacion); // Agregar la cotización insertada a la lista
        //        }
        //    }

        //    return insertedCotizaciones;
        //}
        //public void InsertQuotationSF(CotizacionSFViewModel cotizaciones, string connectionUrls)
        //{
        //    //var ub = cotizaciones.First().Ubicacion;


        //    return ExecuteWithRetry(() =>
        //    {

        //        var request = new RestRequest(_connectionString, Method.Post);
        //        request.AddHeader("Content-Type", "application/json");
        //        request.AddParameter("application/json", JsonConvert.SerializeObject(salesOrder), ParameterType.RequestBody);

        //        RestClient client = InitRestClientAuthConfigs(NetsuiteConst.Request.Urls.DTDEV_SALES_ORDER);
        //        IRestResponse response = client.Execute(request);
        //        int id = int.Parse(response.Content);
        //        return id;
        //    });
        //}

        public T ExecuteWithRetry<T>(Func<T> action, int maxRetryCount = 3, int secondsRetryInterval = 5)
        {
            TimeSpan retryInterval = TimeSpan.FromSeconds(secondsRetryInterval);
            string lastError = "Antes de entrar al ciclo ExecuteWithRetry";

            for (int retry = 1; retry <= maxRetryCount; retry++)
            {
                try
                {
                    T result = action.Invoke();
                    return result;
                }
                catch (Exception ex)
                {
                    lastError = ex.Message;
                }

                if (retry < maxRetryCount)
                {
                    Thread.Sleep(retryInterval);
                }
                else
                {
                    throw new Exception(lastError);
                }
            }
            throw new Exception(lastError);
        }

        public List<CotizacionSFViewModel> InsertQuotationSF(List<CotizacionSFViewModel> cotizaciones)
        {

            return ExecuteWithRetry(() =>
            {
                // Crear la solicitud
                var request = new RestRequest(_connectionUrls, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(cotizaciones), ParameterType.RequestBody);

                // Inicializar el cliente REST
                RestClient client = new RestClient(_connectionUrls);

                // Ejecutar la solicitud y obtener la respuesta
                var response = client.Execute<List<CotizacionSFViewModel>>(request);

                // Manejar la respuesta
                if (response.IsSuccessful)
                {
                    // Retornar las cotizaciones si la respuesta es exitosa
                    return response.Data; // Esto devuelve el tipo que has especificado (List<CotizacionSFViewModel>)
                }
                else
                {
                    // Manejo de errores
                    throw new Exception($"Error al insertar cotizaciones: {response.ErrorMessage}");
                }
            });
        }

    
    }

}
