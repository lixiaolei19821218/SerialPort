using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Model
{
    public class OrderLine
    {
        public string SortId { get; set; }
        public string OrderNumber { get; set; }
        public string RetailerId { get; set; }
        public string Retailer { get; set; }
        public string BrandId { get; set; }
        public string Brand { get; set; }
        public int Count { get; set; }
        public string PCCode { get; set; }
        public string OrderSortId { get; set; }
        public string OrderTime { get; set; }
        public string CurrTime { get; set; }
        public string FJROut { get; set; }
        public string RouteId { get; set; }
        public string RouteName { get; set; }
    }
}
