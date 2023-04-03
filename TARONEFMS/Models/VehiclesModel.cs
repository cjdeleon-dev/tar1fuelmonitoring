using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TARONEFMS.Models
{
    public class VehiclesModel
    {
        public int Id { get; set; }
        public string Vehicle { get; set; }
        public string YearAcquired { get; set; }
        public string PlateNo { get; set; }
        public string EngineNo { get; set; }
        public string VehicleNo { get; set; }
        public int MakeId { get; set; }
        public string Make { get; set; }
        public int OfficeId { get; set; }
        public string Office { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}