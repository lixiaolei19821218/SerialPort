using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Model
{
    public class OrderLine
    {
        public string OrderNumber { get; set; }
        public string RetailerId { get; set; }
        public string Retailer { get; set; }
        public string BrandId { get; set; }
        public string Brand { get; set; }
        public int Count { get; set; }
        public string RouteId { get; set; }
        public string RouteName { get; set; }
    }
}
