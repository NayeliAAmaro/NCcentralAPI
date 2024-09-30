using DTQuotationGS.Entities;
<<<<<<< HEAD
using HtmlAgilityPack;
=======
>>>>>>> 50a97b7 (no message)
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
<<<<<<< HEAD
using System.Security.Cryptography;
=======
>>>>>>> 50a97b7 (no message)
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DTQuotationGS.Repository
{
    public class CotizacionSFRepository
    {
        private readonly string _connectionString;
<<<<<<< HEAD
        public decimal tdc;

=======
>>>>>>> 50a97b7 (no message)
        public CotizacionSFRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

<<<<<<< HEAD


        public List<CotizacionSFEntity> GetCotizacionSF(int intervaloMinutos)
        {
            //string url = "<https://www.banxico.org.mx/tipcamb/tipCamMIAction.do>"; // URL del tipo de cambio
            //string fechaActual = DateTime.Now.ToString("dd/MM/yyyy");


            //using (HttpClient client = new HttpClient())
            //{
            //    HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
            //    response.EnsureSuccessStatusCode();
            //    string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            //    // Cargar el HTML en HtmlDocument
            //    var doc = new HtmlDocument();
            //    doc.LoadHtml(responseBody);

            //    // Ajustar el selector según la fecha actual
            //    var tipoCambioNode = doc.DocumentNode.SelectSingleNode($"//table//tr[td[contains(text(),'{fechaActual}')]]/td[4]");

            //    if (tipoCambioNode != null)
            //    {
            //        string tipoCambio = tipoCambioNode.InnerText.Trim();

            //        // Convertir el tipo de cambio a decimal y formatearlo a cuatro decimales
            //        if (decimal.TryParse(tipoCambio, out decimal tipoCambioDecimal))
            //        {
            //            tdc = tipoCambioDecimal;
            //            Console.WriteLine("Tipo de cambio para pagos (fecha " + tipoCambioDecimal + "): " + tipoCambioDecimal.ToString("F2")); // Formato a cuatro decimales
            //        }
            //        else
            //        {
            //            Console.WriteLine("No se pudo convertir el tipo de cambio a decimal.");
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("No se pudo encontrar el tipo de cambio para la fecha actual.");
            //    }

            //}

=======
        public List<CotizacionSFEntity> GetCotizacionSF(int intervaloMinutos)
        {
>>>>>>> 50a97b7 (no message)
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
<<<<<<< HEAD

                                    QuoCurrency = reader["QuoCurrency"].ToString(),
                                    Send = reader.GetBoolean(reader.GetOrdinal("Send")),
=======
                                    QuoCurrency = reader["QuoCurrency"].ToString(),
                                    Send = reader.GetByte(reader.GetOrdinal("Send")),
>>>>>>> 50a97b7 (no message)
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

        public List<CotizacionSFViewModel> InsertQuotationSF(List<CotizacionSFViewModel> cotizaciones)
        {
            var insertedCotizaciones = new List<CotizacionSFViewModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var cotizacion in cotizaciones)
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[STP_InsertQuotation]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de la cotización
                        cmd.Parameters.AddWithValue("@IdQuotation", cotizacion.IdQuotation);
                        cmd.Parameters.AddWithValue("@QuoClient", cotizacion.QuoClient);
                        cmd.Parameters.AddWithValue("@QuoVendor", cotizacion.QuoVendor);
                        cmd.Parameters.AddWithValue("@QuoCreated", cotizacion.QuoCreated);
                        cmd.Parameters.AddWithValue("@QuoExpiration", cotizacion.QuoExpiration);
                        cmd.Parameters.AddWithValue("@QuoTotal", cotizacion.QuoTotal);
                        cmd.Parameters.AddWithValue("@QuoCurrency", cotizacion.QuoCurrency);

                        SqlParameter idMParam = new SqlParameter("@Id", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(idMParam);

                        cmd.ExecuteNonQuery(); // Inserta la cotización

                        int idQuotationGenerated = (int)idMParam.Value;

                        // Insertar detalles
                        foreach (var detalle in cotizacion.Detalles)
                        {
                            using (SqlCommand cmdDetalle = new SqlCommand("[dbo].[STP_InsertQuotationDetail]", conn))
                            {
                                cmdDetalle.CommandType = CommandType.StoredProcedure;

                                // Parámetros de los detalles de la cotización
                                cmdDetalle.Parameters.AddWithValue("@IdMaster", idQuotationGenerated);
                                cmdDetalle.Parameters.AddWithValue("@Model", detalle.Model);
                                cmdDetalle.Parameters.AddWithValue("@Quantity", detalle.Quantity);
                                cmdDetalle.Parameters.AddWithValue("@UnitPriceIVA", detalle.UnitPriceIVA);
                                cmdDetalle.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);

                                cmdDetalle.ExecuteNonQuery(); // Inserta cada detalle
                            }
                        }
                    }

                    insertedCotizaciones.Add(cotizacion); // Agregar la cotización insertada a la lista
                }
            }

            return insertedCotizaciones;
        }

    }

}
