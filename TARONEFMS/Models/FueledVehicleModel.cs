using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TARONEFMS.Models
{
    public class FueledVehicleModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public string Vehicle { get; set; }
        public string VehicleNo { get; set; }
        public string PlateNo { get; set; }
        public string DateFueled { get; set; }
        public double Liter { get; set; }
        public double Amount { get; set; }
        public bool IsCompanyVehicle { get; set; }
        public string SalesInvoiceNo { get; set; }
        public string GasSlipNo { get; set; }
        public string GasStation { get; set; }
        public string GasStationAddress { get; set; }
        public string DateEntry { get; set; }
    }
}