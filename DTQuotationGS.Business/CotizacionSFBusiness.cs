using DTQuotationGS.Entities;
using DTQuotationGS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTQuotationGS.Business
{
    public class CotizacionSFBusiness
    {
        private readonly CotizacionSFRepository _repository;

        public CotizacionSFBusiness(string connectionString)
        {
            _repository = new CotizacionSFRepository(connectionString);
        }

        public List<CotizacionSFEntity> GetCotizacionSF(int intervaloMinutos)
        {
            return _repository.GetCotizacionSF(intervaloMinutos);
        }

        public List<CotizacionSFViewModel> InsertQuotationSF(List<CotizacionSFViewModel> cotizaciones)
        {
            return _repository.InsertQuotationSF(cotizaciones);
        }
    }
}
