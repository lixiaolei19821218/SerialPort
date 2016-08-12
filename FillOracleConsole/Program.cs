using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.IO;

namespace FillOracleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "user id=dzyc;password=dzyc;data source=10.89.128.137:1521/orcl";
            con.Open();

            ScanCodeEntities repo = new ScanCodeEntities();
            var qrcodes = repo.QRCodes.ToList().Where(q => q.DateTime.HasValue && q.DateTime.Value.Date == DateTime.Now.Date);
            
            foreach (QRCode qrcode in qrcodes)
            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                string sql = string.Format("insert into qrcodetable(orderid, qrcode, addtime, sequense) values('{0}', '{1}', '{2}', '{3}')", "LXL", "lay", qrcode.DateTime.Value.ToString("dd-M月-yyyy hh:mm:ss"), qrcode.Sequence);
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            StreamReader sr = new StreamReader("QrCode20160811093813.Order");
            List<OrderLine> orderLines = new List<OrderLine>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] temp = line.Split(',');
                OrderLine ol = new OrderLine() {
                    SortId = temp[0],
                    OrderNumber = temp[1], 
                    RetailerId = temp[2], 
                    Retailer = temp[3], 
                    BrandId = temp[4], 
                    Brand = temp[5],
                    Count = int.Parse(temp[6]),
                    PCCode = temp[7],
                    OrderSortId = temp[8],
                    RouteId = temp[9],
                    RouteName = temp[10],
                    OrderTime = temp[11],
                    CurrTime = temp[12],
                    FJROut = temp[13]
                };
                orderLines.Add(ol);
            }
            sr.Close();
            foreach (OrderLine ol in orderLines)
            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                string sql = string.Format("insert into RETAILERORDER(SORTID, ORDERID, CUSTOMCODE, CUSTOMNAME, CIGARCODE, CIGARNAME, QUANTITY, PCCODE, ORDERSORTID, ORDERTIME, CURRTIME, FJROUT, XLCODE, XLNAME) values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}')", 
                    ol.SortId, ol.OrderNumber, ol.RetailerId, ol.Retailer, ol.BrandId, ol.Brand, ol.Count, ol.PCCode, ol.OrderSortId, ol.OrderTime, ol.CurrTime, ol.FJROut, ol.RouteId, ol.RouteName
                    );
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            
            con.Close();             
            con.Dispose();
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
