namespace DTQuotationGS.Entities
{
    public class CotizacionSFEntity
    {
        public int Id { get; set; }
        public string IdQuotation { get; set; }
        public string QuoClient { get; set; }
        public string QuoVendor { get; set; }
        public DateTime QuoCreated { get; set; }
        public DateTime QuoExpiration { get; set; }
        public decimal QuoTotal { get; set; }
        public string QuoCurrency { get; set; }
        public bool Send { get; set; }

        public List<CotizacionSFEntityDetalle> Detalles { get; set; } = new List<CotizacionSFEntityDetalle>();
    }


    public class CotizacionSFEntityDetalle
    {
        public int IdQuotationDetails { get; set; }
        public int IdMaster { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPriceIVA { get; set; }
        public decimal Subtotal { get; set; }

    }

    public class CotizacionRequest
    {
        public CotizacionSFEntity MasterQuotation { get; set; }
        public List<CotizacionSFEntityDetalle> Details { get; set; }
    }
}
