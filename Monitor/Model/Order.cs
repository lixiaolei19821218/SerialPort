using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Model
{
    public class Order
    {
        public string Number { get; set; }
        public string Retailer { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public int TotalCount { get; set; }
        public string RouteId { get; set; }
        public string RouteName { get; set; }
    }
}
