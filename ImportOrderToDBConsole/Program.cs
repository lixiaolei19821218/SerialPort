using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportOrderToDBConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ScanCodeEntities repo = new ScanCodeEntities();

            string orderFolder = @"c:\order";
            if (!Directory.Exists(orderFolder))
            {
                Directory.CreateDirectory(orderFolder);
            }
            string dayFolder = Path.Combine(orderFolder, DateTime.Today.ToString("yyyyMMdd"));
            if (!Directory.Exists(dayFolder))
            {
                Directory.CreateDirectory(dayFolder);
            }
            string[] orderFiles = Directory.GetFiles(dayFolder, "*.Order", SearchOption.TopDirectoryOnly);
            StreamReader sr = new StreamReader(orderFiles[0]);

            List<OrderLine> orderLines = new List<OrderLine>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] temp = line.Split(',');
                OrderLine ol = new OrderLine() { OrderNumber = temp[1], RetailerId = temp[2], Retailer = temp[3], BrandId = temp[4], Brand = temp[5], Count = int.Parse(temp[6]), RouteId = temp[9], RouteName = temp[10], FJROut = temp[13] };
                if (ol.FJROut == "002")
                {
                    orderLines.Add(ol);
                }
            }
            sr.Close();
            var orders = from ol in orderLines group ol by ol.OrderNumber into g select new Order() { OrderNumber = g.Key, Count = g.Sum(o => o.Count), Date = DateTime.Today };
            foreach (Order order in orders)
            {
                repo.Orders1.Add(order);
            }
            repo.SaveChanges();
        }
    }

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
