using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTQuotationGS.Entities
{
    public class CotizacionSFViewModel
    {
        public string IdQuotation { get; set; }
        public string QuoClient { get; set; }
        public string QuoVendor { get; set; }
        public DateTime QuoCreated { get; set; }
        public DateTime QuoExpiration { get; set; }
        public decimal QuoTotal { get; set; }
        public string QuoCurrency { get; set; }

<<<<<<< HEAD

        public string Ubicacion { get; set; }
=======
        //public string ub { get; set; }

>>>>>>> 50a97b7 (no message)
        public List<CotizacionSFDetalleViewModel> Detalles { get; set; } = new List<CotizacionSFDetalleViewModel>();
    }


    public class CotizacionSFDetalleViewModel
    {
        public string Model { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPriceIVA { get; set; }
        public decimal Subtotal { get; set; }

    }

}
