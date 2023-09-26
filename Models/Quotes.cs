using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Demo.Models
{
    public class Quotes
    {
        public string from { get; set; }
        public string to { get; set; }

        public List<VehicleList> listings { get; set; }
    }

    public class VehicleList
    {
        public string name { get; set; }
        public double pricePerPassenger { get; set; }

        public Type vehicleType { get; set; }

        public double totalprice { get; set; }

    }

    public class Type
    {
        public string name { get; set; }
        public int maxPassengers { get; set; }
    }
}