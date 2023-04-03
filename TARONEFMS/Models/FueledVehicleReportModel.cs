using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TARONEFMS.Models
{
    public class FueledVehicleReportModel
    {
        public string Vehicle { get; set; }
        public string PlateNo { get; set; }
        public string Office { get; set; }
        public string Date { get; set; }
        public double Liters { get; set; }
        public double Amount { get; set; }
    }
}